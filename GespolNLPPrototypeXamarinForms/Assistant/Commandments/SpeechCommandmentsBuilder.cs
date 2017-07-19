using System;
using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Assistant.Fragments.Converters;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;

namespace XamarinFormsAssistant.Assistant.Commandments
{
    public class SpeechCommandmentsBuilder
    {
        /// <summary>
        /// Given a raw text returns a List of Commandments ready to execute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<SpeechCommandment> Build(string text)
        {
            var splittedSpeechFragments = AnalyzeRawText(text);

            var splittedPossibleCommandments = SplitSpeechFragmentsIntoPossibleCommandments(splittedSpeechFragments);

            var resultSpeechCommandments = BuildRawCommandments(splittedPossibleCommandments);

            resultSpeechCommandments = FindImplicitUpdateCommandsAndCleanSyntax(resultSpeechCommandments);

            DeleteInvalidSpeechCommandments(ref resultSpeechCommandments);

            return resultSpeechCommandments;
        }

        /// <summary>
        /// Given a commandments list it deletes invalid syntax commandments from it
        /// </summary>
        /// <param name="commandments"></param>
        private void DeleteInvalidSpeechCommandments(ref List<SpeechCommandment> commandments)
        {
            var invalidHashList = new List<int>();
            for (var index = 0; index < commandments.Count; index++)
            {
                var commandment = commandments[index];
                if (!commandment.IsSyntaxValid())
                {
                    invalidHashList.Add(commandments[index].GetHashCode());
                }
            }

            foreach (var invalidHash in invalidHashList)
            {
                commandments.Remove(commandments.Single(commandment => commandment.GetHashCode() == invalidHash));
            }
        }

        /// <summary>
        /// Finds possible internal commandments inside additional parameters and cleans syntax (Example: Nueva Denuncia 1234 ---> Nueva Denuncia)
        /// </summary>
        /// <param name="resultSpeechCommandments"></param>
        /// <returns></returns>
        private List<SpeechCommandment> FindImplicitUpdateCommandsAndCleanSyntax(List<SpeechCommandment> resultSpeechCommandments)
        {
            var result = new List<SpeechCommandment>();

            foreach (var speechCommandment in resultSpeechCommandments)
            {
                var subcommandments = speechCommandment.FindSubCommandments();
                result.Add(speechCommandment.ClearSyntax());
                if(subcommandments != null)
                    result.AddRange(subcommandments);
            }
            return result;
        }

        /// <summary>
        /// Given a List of ListSpeechFragment it returns a list of SpeechCommandments, setting up MainCommand, MainParameter and AdditionalParameters
        /// </summary>
        /// <param name="splittedPossibleCommandments"></param>
        /// <returns></returns>
        private List<SpeechCommandment> BuildRawCommandments(List<List<SpeechFragment>> splittedPossibleCommandments)
        {
            var resultSpeechCommandments = new List<SpeechCommandment>();
            foreach (var splittedPossibleCommandment in splittedPossibleCommandments)
            {
                var speechCommandment = new SpeechCommandment();
                foreach (var speechFragment in splittedPossibleCommandment)
                {
                    if (speechFragment is SpeechCommand)
                    {
                        speechCommandment.MainCommand = speechFragment as SpeechCommand;
                    }
                    else
                    {
                        if (speechFragment is SpeechParameter && speechCommandment.MainParameter == null)
                        {
                            speechCommandment.MainParameter = speechFragment as SpeechParameter;
                        }
                        else
                        {
                            speechCommandment.AdditionalParameters.Add(speechFragment as SpeechParameter);
                        }
                    }
                }
                resultSpeechCommandments.Add(speechCommandment);

            }
            return resultSpeechCommandments;
        }

        /// <summary>
        /// Given a list of SpeechFragments it returns a List of Splitted List SpeechFragments. (Necessary Input for BuildRawCommandments)
        /// </summary>
        /// <param name="splittedSpeechFragments"></param>
        /// <returns></returns>
        private List<List<SpeechFragment>> SplitSpeechFragmentsIntoPossibleCommandments(List<SpeechFragment> splittedSpeechFragments)
        {
            var result = new List<List<SpeechFragment>>();

            bool commandTaken = false;
            bool previousWasDeleteCommand = false;
            var internalList = new List<SpeechFragment>();
            for (var i = 0; i < splittedSpeechFragments.Count; i++)
            {
                splittedSpeechFragments[i] = CastToSpeechCommandIfItIsContextProperty(Assistant.GetInstance().CurrentContextType, splittedSpeechFragments[i]);
                ClassifySpeechFragment(splittedSpeechFragments, result, ref commandTaken, ref previousWasDeleteCommand, ref internalList, i);
            }
            result.Add(internalList);

            return result;
        }

        /// <summary>
        /// Given an splittedSpeechFragments list and index, it updates result or internal list according to commandTaken Boolean and SpeechFragmentType
        /// </summary>
        /// <param name="splittedSpeechFragments"></param>
        /// <param name="result"></param>
        /// <param name="commandTaken"></param>
        /// <param name="internalList"></param>
        /// <param name="index"></param>
        private static void ClassifySpeechFragment(List<SpeechFragment> splittedSpeechFragments, List<List<SpeechFragment>> result, ref bool commandTaken, ref bool previousWasDeleteCommand, ref List<SpeechFragment> internalList, int index)
        {
            if (splittedSpeechFragments[index] is SpeechParameter
                                && ((SpeechParameter)splittedSpeechFragments[index]).SpeechParameterType == SpeechParameterType.Property
                                && commandTaken && !previousWasDeleteCommand)
            {
                result.Add(internalList);
                internalList = new List<SpeechFragment>();
                commandTaken = false;
                previousWasDeleteCommand = false;
            }
            if (splittedSpeechFragments[index] is SpeechCommand && commandTaken)
            {
                result.Add(internalList);
                internalList = new List<SpeechFragment>();
                commandTaken = false;
                previousWasDeleteCommand = false;
            }

            if (splittedSpeechFragments[index] is SpeechCommand && !commandTaken)
            {
                internalList.Add(splittedSpeechFragments[index]);
                commandTaken = true;
                previousWasDeleteCommand = ((SpeechCommand) splittedSpeechFragments[index]).SpeechCommandType ==
                                           SpeechCommandType.Delete;
            }

            if (commandTaken && splittedSpeechFragments[index] is SpeechParameter)
            {
                internalList.Add(splittedSpeechFragments[index]);
                previousWasDeleteCommand = false;
            }

            if (!commandTaken && splittedSpeechFragments[index] is SpeechParameter
                && ((SpeechParameter)splittedSpeechFragments[index]).SpeechParameterType == SpeechParameterType.Property)
            {
                internalList.Add(new SpeechCommand() { SpeechCommandType = SpeechCommandType.Update });
                internalList.Add(splittedSpeechFragments[index]);
                commandTaken = true;
                previousWasDeleteCommand = false;
            }
        }

        /// <summary>
        /// If splittedSpeechFragment MainParameter OriginalText is equal to any AssistantPropertySpeechNamesAttribute it returns a new SpeechParameter with Type Property.
        /// </summary>
        /// <param name="actualContextType"></param>
        /// <param name="splittedSpeechFragment"></param>
        /// <returns></returns>
        private SpeechFragment CastToSpeechCommandIfItIsContextProperty(Type actualContextType, SpeechFragment splittedSpeechFragment)
        {
            if (actualContextType == null) return splittedSpeechFragment;

            var textToUpper = splittedSpeechFragment.OriginalTextFragment.ToUpper();

                if(AssistantSpeechNamesManager.ExistsSpeechName(actualContextType,textToUpper))
                {
                    return new SpeechParameter()
                    {
                        OriginalTextFragment = splittedSpeechFragment.OriginalTextFragment,
                        SpeechParameterType = SpeechParameterType.Property
                    };
                }

            return splittedSpeechFragment;
        }

        /// <summary>
        /// Returns a raw SpeechFragment List from a given text input
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<SpeechFragment> AnalyzeRawText(string text)
        {
            var speechFragmentList = new List<SpeechFragment>();
            var splittedText = text.Split(' ');
            foreach (var s in splittedText)
            {
                speechFragmentList.Add(SpeechConverter.Convert(s));
            }
            return speechFragmentList;

        }
    }
}