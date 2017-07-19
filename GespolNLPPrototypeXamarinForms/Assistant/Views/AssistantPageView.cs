using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Converters;
using XamarinFormsAssistant.Assistant.Global.Animations;
using XamarinFormsAssistant.Assistant.Global.Styles;
using XamarinFormsAssistant.Assistant.Viewmodels;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Views
{
    public class AssistantPageView : ContentPage
    {
        public AssistantPageViewModel Context;

        #region LayoutProperties

        public StackLayout MandatoryFieldsStackLayout { get; set; } = new StackLayout();

        public StackLayout FooterStack { get; set; } = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            VerticalOptions = LayoutOptions.EndAndExpand
        };

        public HeaderLabelStack HeaderLabelStack { get; set; }

        public ScrollView TutorialScroll { get; set; } = new ScrollView();

        #endregion

        #region UI Properties



        public ListView MandatoryFieldsListView { get; set; } = new ListView();

        public Button AcceptButton { get; set; } = new Button()
        {
            Text = AssistantResources_ES.Accept,
            Command = new Command(() => { }),
            Style = AssistantStyles.AcceptButton
        };

        public Button ExitButton { get; set; } = new Button()
        {

            Command = new Command(() =>
            {
                /*Navigation.PopAsync(true)*/
            }),
            Style = AssistantStyles.CancelButton,
            Text = AssistantResources_ES.Exit,
        };

        public AssistantCube AssistantCube { get; set; } = new AssistantCube();

        #endregion

        public bool AlreadyAppeared { get; set; }

        public AssistantPageView(AssistantPageViewModel context)
        {
            Context = context;
            BindingContext = Context;

            InitializeUIComponents();
            WireUIEvents();

           
            

            MandatoryFieldsListView.ItemTemplate = new DataTemplate(() =>
            {
                var viewCell = new AnimatableViewCell();


                viewCell.Appearing += (sender, args) =>
                {
                    if (!AlreadyAppeared)
                    {
                        viewCell.AssistantCell.Animate("Appear", d => viewCell.AssistantCell.Scale = d, 0D, 1D, 16U, 500U,
                            Easing.SinIn, (d, b) => AlreadyAppeared = true);
                        
                    }
                };

                
                return viewCell;
            });

            MandatoryFieldsListView.SetBinding(ListView.ItemsSourceProperty,
                nameof(AssistantPageViewModel.MandatoryFieldsStringList));
            AcceptButton.SetBinding(IsVisibleProperty, nameof(AssistantPageViewModel.CurrentContextName),
                BindingMode.OneWay, new EmptyStringToBooleanConverter());

            WireAnimations();

            MandatoryFieldsStackLayout.Children.Add(MandatoryFieldsListView);

            FooterStack.Children.Add(AcceptButton);

            FooterStack.Children.Add(ExitButton);

            Content = new StackLayout()
            {
                Children =
                {
                    HeaderLabelStack,
                    TutorialScroll,
                    MandatoryFieldsStackLayout,
                    AssistantCube,
                    FooterStack
                }
            };
        }

        private void InitializeUIComponents()
        {
            HeaderLabelStack = new HeaderLabelStack(nameof(AssistantPageViewModel.CurrentContextName));
            MandatoryFieldsListView.HasUnevenRows = true;
            MandatoryFieldsStackLayout.SetBinding(IsVisibleProperty,nameof(AssistantPageViewModel.CurrentContextName),BindingMode.Default,new IsVisibleTutorialConverter(inverse:true));
            InitializeTutorialStack();
        }

        private void InitializeTutorialStack()
        {
            var tutorialStack = new StackLayout();
            var tutorialCell1 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText1}};
            var tutorialCell2 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText2}};
            var tutorialCell3 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText3}};
            var tutorialCell4 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText4}, MainFrame = {BackgroundColor = Color.FromRgb(40, 40, 128) }};
            var tutorialCell5 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText5}};
            var tutorialCell6 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText6}, MainFrame = { BackgroundColor = Color.FromRgb(40, 40, 128) } };
            var tutorialCell7 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText7}};
            var tutorialCell8 = new AssistantCell {MainLabel = {Text = AssistantResources_ES.TutorialText8}};
            tutorialStack.Children.Add(tutorialCell1);
            tutorialStack.Children.Add(tutorialCell2);
            tutorialStack.Children.Add(tutorialCell3);
            tutorialStack.Children.Add(tutorialCell4);
            tutorialStack.Children.Add(tutorialCell5);
            tutorialStack.Children.Add(tutorialCell6);
            tutorialStack.Children.Add(tutorialCell7);
            tutorialStack.Children.Add(tutorialCell8);

            TutorialScroll.Content = tutorialStack;
            TutorialScroll.SetBinding(IsVisibleProperty, nameof(AssistantPageViewModel.CurrentContextName), BindingMode.Default, new IsVisibleTutorialConverter());
        }

        private void WireUIEvents()
        {
            SizeChanged += (sender, args) =>
            {
                MandatoryFieldsListView.HeightRequest = this.Height;
            };
        }

        private void WireAnimations()
        {
            MessagingCenter.Subscribe<string>(this, AssistantAnimations.BackgroundColor.OnName, s =>
            {
                AssistantAnimations.BackgroundColor.AnimateOn(this, "BackgroundColor");
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.BackgroundColor.OffName, s =>
            {
                AssistantAnimations.BackgroundColor.AnimateOff(this, "BackgroundColor");
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.AssistantCube.OnName, s =>
            {
                AssistantAnimations.AssistantCube.AnimateOn(this, "AssistantCube", AssistantCube);
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.AssistantCube.OffName, s =>
            {
                AssistantAnimations.AssistantCube.AnimateOff(this, "AssistantCube",
                    AssistantCube);
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.AssistantCubeAppear.OnName, s =>
            {
                AssistantAnimations.AssistantCubeAppear.AnimateOn(this, "AssistantCubeAppear",
                    AssistantCube);
            });

            MessagingCenter.Subscribe<string>(this, AssistantAnimations.AssistantCubeAppear.OffName, s =>
            {
                AssistantAnimations.AssistantCubeAppear.AnimateOff(this, "AssistantCubeAppear",
                    AssistantCube);
            });


        }

    }
}