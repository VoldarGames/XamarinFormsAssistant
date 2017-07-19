using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Commandments;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Syntax.Factory;

namespace XamarinFormsAssistant.Assistant.Syntax
{
    public static class CommandmentSyntaxDefinitions
    {

        public static Dictionary<SpeechCommandType,SyntaxDefinition> SyntaxDefinitions = new Dictionary<SpeechCommandType, SyntaxDefinition>()
        {
            {SpeechCommandType.Create, SyntaxDefinitionFactory.CreateSpeechCommandSyntax },
            {SpeechCommandType.Update, SyntaxDefinitionFactory.UpdateSpeechCommandSyntax },
            {SpeechCommandType.Delete, SyntaxDefinitionFactory.DeleteSpeechCommandSyntax },
            {SpeechCommandType.Accept, SyntaxDefinitionFactory.AcceptSpeechCommandSyntax },
            {SpeechCommandType.Cancel, SyntaxDefinitionFactory.CancelSpeechCommandSyntax },
            {SpeechCommandType.Reset, SyntaxDefinitionFactory.ResetSpeechCommandSyntax },
            {SpeechCommandType.Response, SyntaxDefinitionFactory.ResponseSpeechCommandSyntax },
            {SpeechCommandType.Repeat, SyntaxDefinitionFactory.RepeatSpeechCommandSyntax }

        };

        /// <summary>
        /// Returns true if speechCommandment matches syntax definition
        /// </summary>
        /// <param name="speechCommandment"></param>
        /// <returns></returns>
        public static bool IsSyntaxValid(SpeechCommandment speechCommandment)
        {
            if (speechCommandment.MainCommand == null) return false;
            var correctSyntax = SyntaxDefinitions[speechCommandment.MainCommand.SpeechCommandType].SpeechTypesSyntax;
            for (int i = 0; i < correctSyntax.Count; i++)
            {
                if (i == 0 && (SpeechCommandType) correctSyntax[i] != speechCommandment.MainCommand?.SpeechCommandType)
                    return false;
                if(i == 1 && (SpeechParameterType)correctSyntax[i] != speechCommandment.MainParameter?.SpeechParameterType)
                    return false;
                if (i == 2 && speechCommandment.AdditionalParameters != null 
                    && speechCommandment.AdditionalParameters
                        .All(parameter => parameter.SpeechParameterType != (SpeechParameterType) correctSyntax[i]))
                    return false;

            }
            return true;
        }
    }
}