using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Manager;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;
using XamarinFormsAssistant.Assistant.Views;
using XamarinFormsAssistant.Prototype.Application;

namespace XamarinFormsAssistant.Assistant.Viewmodels
{
    public class AssistantPageViewModel : BindableObject, IAssistantSelectionPageViewModel
    {
        private string _currentContextName;

        public string CurrentContextName
        {
            get { return _currentContextName; }
            set
            {
                _currentContextName = value;
                OnPropertyChanged();
            }
        }
        public string BackgroundAnimationName { get; } = "BackgroundColorModal";
        public ICommand CancelCommand { get; set; } = new Command(() => MessagingCenter.Send(AssistantMessagingOrder.CancelSearch, AssistantMessagingOrder.CancelSearch));
        public string HeaderSuffix { get; } = AssistantSpeechNamesManager.GetPropertyHeader(Assistant.GetInstance().CurrentContextPropertyTypeName);

        public ObservableCollection<object> SelectionResults { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<string> MandatoryFieldsStringList { get; set; } = new ObservableCollection<string>();

        public AssistantPageViewModel()
        {
            Assistant.GetInstance().InitializeContext(this);
            WireMessages();
        }

        private void WireMessages()
        {
            MessagingCenter.Subscribe<string>(this, AssistantMessaging.ItemSelectedOnResults,
            s =>
            {
                NavigationService.Navigation.PopModalAsync();
            });

            MessagingCenter.Subscribe<string>(this, AssistantMessaging.ModuleNotFound, s =>
            {
                NavigationService.Navigation.PushModalAsync(new AssistantSelectionModulesPageViewModel().GetView());
            });
        }

        public void SetRequiredFields()
        {
            MandatoryFieldsStringList.Clear();
            if (Assistant.GetInstance()
                .CurrentContextType == null) return;

            SetRequiredFieldsInternal(Assistant.GetInstance().CurrentContextType);
        }

        private void SetRequiredFieldsInternal(Type currentType)
        {
            var requiredProperties = currentType.GetProperties()
                                .Where(info => info.GetCustomAttribute<AssistantRequiredFieldAttribute>() != null
                                               || info.GetCustomAttribute<AssistantRequiredNavigationField>() != null);
            foreach (var requiredProperty in requiredProperties)
            {
                if (requiredProperty.GetCustomAttribute<AssistantRequiredNavigationField>() != null)
                {
                    SetRequiredFieldsInternal(requiredProperty.PropertyType);
                }
                else
                {
                    var header =
                        AssistantSpeechNamesManager.GetPropertyHeader(requiredProperty.Name);
                    object value = null;
                    if (Assistant.GetInstance().CurrentInstanceResult != null)
                    {
                        value = GetInstanceInCurrentContextForRequiredProperty(requiredProperty);
                        if(value != null) value = requiredProperty.GetValue(value);
                    }
                    MandatoryFieldsStringList.Add($"{header}: {value ?? "*Sin valor*"}");
                }
            }
        }

        private object GetInstanceInCurrentContextForRequiredProperty(PropertyInfo requiredProperty)
        {
            var currentInstance = Assistant.GetInstance().CurrentInstanceResult;

            if (currentInstance.GetType().GetProperties().FirstOrDefault(info => info.Equals(requiredProperty)) != null)
                return currentInstance;

            return GetInstanceInCurrentContextForRequiredPropertyInternal(requiredProperty, currentInstance);
        }

        private object GetInstanceInCurrentContextForRequiredPropertyInternal(PropertyInfo requiredProperty, object currentInstance)
        {
            foreach (var property in currentInstance.GetType().GetProperties())
            {
                if(property.Equals(requiredProperty)) return currentInstance;

                if (property.GetCustomAttribute<AssistantRequiredNavigationField>() != null)
                {
                    var result = GetInstanceInCurrentContextForRequiredPropertyInternal(requiredProperty,
                        property.GetValue(currentInstance));
                    if(result != null)return result;
                }
            }
            return null;
        }

        public void SetSelectionResults()
        {
            SelectionResults.Clear();
            foreach (var selectionResult in Assistant.GetInstance().SelectionResults)
            {
                SelectionResults.Add(selectionResult);
            }
            if (SelectionResults.Count > 1 && Assistant.GetInstance().CurrentWaitingResponseType == WaitingResponseType.None)
            {
                NavigationService.Navigation.PushModalAsync(new AssistantSelectionResultsPageView(this));
            }
        }

        public void SetHeaderText(string value)
        {
            CurrentContextName = value;
        }

        public void DisposeSelectionListView()
        {
            SelectionResults.Clear();
        }
        
        public void OnSelectionResultsListViewOnItemTapped(object sender, ItemTappedEventArgs args)
        {
            var currentInstanceType = Assistant.GetInstance().CurrentInstanceResult.GetType();
            var currentInstanceTypePrperty =
                currentInstanceType.GetPropertyInChildrenWithAttribute<AssistantRequiredFieldAttribute>(Assistant.GetInstance().CurrentContextPropertyTypeName);
                    //.GetProperty(NLPPrototypeXamarinForms.Assistant.Assistant.GetInstance().CurrentContextPropertyTypeName,
                    //    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            //currentInstanceTypePrperty.SetValue(NLPPrototypeXamarinForms.Assistant.Assistant.GetInstance().CurrentInstanceResult,
            //    args.Item);
            currentInstanceTypePrperty.SetChildrenValue(args.Item);
            DependencyService.Get<IOperatingSystemMethods>().Vibrate(500);
            Assistant.GetInstance().ClearWaitingResponse();
            Assistant.GetInstance().CurrentInstanceResultChanged();
            MessagingCenter.Send(AssistantMessaging.ItemSelectedOnResults, AssistantMessaging.ItemSelectedOnResults);
        }

        public Page GetView()
        {
            return new AssistantPageView(this);
        }

        public void SetRequiredField(string headerTextOfPropertyChanged, string newValue)
        {
            var changedIndex = MandatoryFieldsStringList.IndexOf(MandatoryFieldsStringList.Single(s => s.StartsWith(headerTextOfPropertyChanged)));
            MandatoryFieldsStringList[changedIndex] = newValue;
        }
    }
}