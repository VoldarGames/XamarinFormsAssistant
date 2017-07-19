using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.Enums;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Manager
{
    public class CommandmentExecutorManager
    {
        private readonly Dictionary<SpeechCommandType, CommandmentExecutorBase> _commandmentExecutorDictionary = new Dictionary<SpeechCommandType, CommandmentExecutorBase>()
        {
            {SpeechCommandType.Create, new CreateCommandmentExecutor() },
            {SpeechCommandType.Update, new UpdateCommandmentExecutor() },
            {SpeechCommandType.Delete, new DeleteCommandmentExecutor() },
            {SpeechCommandType.Accept, new AcceptCommandmentExecutor() },
            {SpeechCommandType.Cancel, new CancelCommandmentExecutor() },
            {SpeechCommandType.Reset, new ResetCommandmentExecutor() },
            {SpeechCommandType.Response, new ResponseCommandmentExecutor() },
            {SpeechCommandType.Repeat, new RepeatCommandmentExecutor() },
        };

        /// <summary>
        /// Given a list of SpeechCommandments it holds conflictive SpeechCommandments into PendingCommandments List and executes safely the rest
        /// </summary>
        /// <param name="commandments"></param>
        public void Execute(List<SpeechCommandment> commandments)
        {
            PushNecessaryPendingCommandments(ref commandments);
            ClearPendingCommandmentsFromCurrentCommandmentList(ref commandments);
            SafeExecuteCommandments(ref commandments);
        }

        /// <summary>
        /// Executes commandments with an specific and correct order
        /// </summary>
        /// <param name="commandments"></param>
        private void SafeExecuteCommandments(ref List<SpeechCommandment> commandments)
        {
            SpeechCommandment waitResponseCommandment = null;
            foreach (var speechCommandment in commandments)
            {
                if (speechCommandment.MainCommand.SpeechCommandType == SpeechCommandType.Update
                    &&
                    Assistant.GetInstance()
                        .CurrentContextType
                        .GetPropertyInChildrenWithAttribute<AssistantSearchFieldAttribute>
                        (speechCommandment.MainParameter.OriginalTextFragment) != null)
                {
                    waitResponseCommandment = speechCommandment;
                    continue;
                }
                ExecuteCommandment(speechCommandment);
            }
            if (waitResponseCommandment != null) ExecuteCommandment(waitResponseCommandment);
        }

        /// <summary>
        /// Removes all pending commandments from 'commandments' param
        /// </summary>
        /// <param name="commandments"></param>
        private static void ClearPendingCommandmentsFromCurrentCommandmentList(ref List<SpeechCommandment> commandments)
        {
            foreach (var pending in Assistant.GetInstance().PendingCommandments)
            {
                commandments.Remove(pending);
            }
        }

        /// <summary>
        /// If commandments param have more than one WaitResponseCommandment they are pushed to PendingCommandments.
        /// PendingCommandments will be executed when WaitResponseType is setted to None.
        /// </summary>
        /// <param name="commandments"></param>
        private void PushNecessaryPendingCommandments(ref List<SpeechCommandment> commandments)
        {
            var waitResponseCommandments = GetWaitResponseCommandments(commandments);

            if (waitResponseCommandments.Count > 1)
            {
                foreach (var waitResponseCommandment in waitResponseCommandments.Skip(1))
                {
                    Assistant.GetInstance().PendingCommandments.Add(waitResponseCommandment);
                }
            }
        }

        /// <summary>
        /// Given an SpeechCommandment List it returns another SpeechCommandments List that represent the collection of WaitResponseCommandments inside commandments param.
        /// </summary>
        /// <param name="commandments"></param>
        /// <returns></returns>
        private List<SpeechCommandment> GetWaitResponseCommandments(List<SpeechCommandment> commandments)
        {
            return commandments.Where(
                commandment =>
                    commandment.MainCommand.SpeechCommandType == SpeechCommandType.Update &&

                    Assistant.GetInstance()
                        .CurrentContextType
                        .GetPropertyInChildrenWithAttribute<AssistantSearchFieldAttribute>
                        (commandment.MainParameter.OriginalTextFragment) != null)
                        .ToList();
        }

        /// <summary>
        /// If commandment has Syntax Valid it will be executed by its Executor.
        /// </summary>
        /// <param name="commandment"></param>
        private void ExecuteCommandment(SpeechCommandment commandment)
        {
            if (commandment.IsSyntaxValid())
            {
                Handle(commandment);
            }
        }

        /// <summary>
        /// Redirects commandment to its specific executor.
        /// </summary>
        /// <param name="commandment"></param>
        private void Handle(SpeechCommandment commandment)
        {
            _commandmentExecutorDictionary[commandment.MainCommand.SpeechCommandType].Execute(commandment);
        }
    }
}