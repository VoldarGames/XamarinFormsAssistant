using System;
using System.Collections.Generic;
using Plugin.TextToSpeech;
using XamarinFormsAssistant.Assistant.AdvisorInterface;
using XamarinFormsAssistant.Assistant.Commandments;
using XamarinFormsAssistant.Assistant.Commandments.Executors.Manager;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Assistant.Helper;
using XamarinFormsAssistant.Assistant.Modules;
using XamarinFormsAssistant.Assistant.Orders.Manager;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;
using XamarinFormsAssistant.Assistant.Viewmodels;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant
{
    public class Assistant
    {
        private static Assistant _instance;

        public AssistantPageViewModel CurrentContext { get; set; }
        public SpeechCommandmentsBuilder SpeechCommandmentsBuilder { get; set; }
        public CommandmentExecutorManager CommandExecutorManager { get; set; }
        public AssistantMessagingOrderManager AssistantMessagingOrderManager { get; set; } = new AssistantMessagingOrderManager();
        public IAssistantAdvisor Advisor { get; set; }
        public object CurrentInstanceResult { get; set; }

        public Type CurrentContextType { get; set; }

        public string CurrentContextTypeName
        {
            get { return _currentContextTypeName; }
            set
            {
                _currentContextTypeName = value;
                if (CurrentContext != null)
                {
                    CurrentContext.SetHeaderText(value);
                    CurrentInstanceResultChanged();
                    CrossTextToSpeech.Current.Speak($"Iniciando {value}");
                }
            }
        }

        public List<SpeechCommandment> PendingCommandments { get; set; } = new List<SpeechCommandment>();

        public string CurrentContextPropertyTypeName { get; set; } = null;

        private WaitingResponseType _currentWaitingResponseType = WaitingResponseType.None;
        private string _currentContextTypeName = "";


        public WaitingResponseType CurrentWaitingResponseType
        {
            get { return _currentWaitingResponseType; }
            set
            {
                _currentWaitingResponseType = value;
                WaitingResponseFeedback(value);
                if (value == WaitingResponseType.None)
                {
                    var pendingCommandments = new List<SpeechCommandment>(PendingCommandments);
                    PendingCommandments.Clear();
                    ExecuteCommandments(pendingCommandments);
                }
            }
        }
        private IList<EntityBase> _selectionResults;

        public IList<EntityBase> SelectionResults
        {
            get { return _selectionResults; }
            set
            {
                _selectionResults = value;
                SelectionResultsChanged();
            }
        }

        private void SelectionResultsChanged()
        {
            if (SelectionResults == null)
            {
                CurrentContext.DisposeSelectionListView();
                return;
            }
            CurrentContext.SetSelectionResults();
        }


        private void WaitingResponseFeedback(WaitingResponseType value)
        {
            WaitingResponseFeedbackHandler.Handle(value);
        }


        protected Assistant()
        {
            SpeechCommandmentsBuilder = new SpeechCommandmentsBuilder();
            CommandExecutorManager = new CommandmentExecutorManager();
            AssistantSpeechNamesManager.InitializeSpeechNames();
            AssistantModuleNamesManager.InitializeModuleNames();

        }
        /// <summary>
        /// Singleton accessor
        /// </summary>
        /// <returns>Returns the current instance of Assistant.</returns>
        public static Assistant GetInstance()
        {
            return _instance ?? (_instance = new Assistant());
        }

        public void InitializeContext(AssistantPageViewModel context)
        {
            CurrentContext = context;
        }

        public void SetAdvisor(IAssistantAdvisor advisor)
        {
            Advisor = advisor;
        }

        /// <summary>
        /// Giving raw text it interprets commandments and execute them if it is possible. Then Assistant gives you advice if it is setted.
        /// </summary>
        /// <param name="text"></param>
        public void Interpret(string text)
        {
            text = DiacriticRemoverHelper.RemoveDiacritics(text);
            
            if (CurrentWaitingResponseType == WaitingResponseType.None)
            {
                var commandments = SpeechCommandmentsBuilder.Build(text);
                ExecuteCommandments(commandments);
                Advisor?.PerformFeedback();
            }
            else
            {
                var commandment = new List<SpeechCommandment>()
                {
                    new SpeechCommandment()
                    {
                        MainCommand = new SpeechCommand()
                        {
                            SpeechCommandType = SpeechCommandType.Response
                        },
                        MainParameter = new SpeechParameter()
                        {
                            SpeechParameterType = SpeechParameterType.Text,
                            OriginalTextFragment = text
                        }
                    }
                };

                ExecuteCommandments(commandment);

            }
        }
        /// <summary>
        /// Executes a list of speech commandments if it is possible
        /// </summary>
        /// <param name="commandments"></param>
        private void ExecuteCommandments(List<SpeechCommandment> commandments)
        {
            CommandExecutorManager.Execute(commandments);
        }

        public void Reset()
        {
            CurrentInstanceResult = null;
            CurrentContextType = null;
            CurrentContextTypeName = "";
        }

        public void CurrentInstanceResultChanged()
        {
            CurrentContext.SetRequiredFields();
        }

        public void CurrentInstanceResultChanged(string headerTextOfPropertyChanged, string newValue)
        {
            CurrentContext.SetRequiredField(headerTextOfPropertyChanged, newValue);
        }

        public void ClearWaitingResponse()
        {
            CurrentWaitingResponseType = WaitingResponseType.None;
        }
        
    }
}