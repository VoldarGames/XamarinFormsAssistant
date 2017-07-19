using System;
using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Enums;

namespace XamarinFormsAssistant.Assistant.Syntax.Factory
{
    public static class SyntaxDefinitionFactory
    {
        public static SyntaxDefinition CreateSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Create,
                SpeechParameterType.Text,
            }
        };

        public static SyntaxDefinition UpdateSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Update,
                SpeechParameterType.Property,
                SpeechParameterType.Text,
            }
        };

        public static SyntaxDefinition DeleteSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Delete,
                SpeechParameterType.Property,
            }
        };

        public static SyntaxDefinition AcceptSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Accept,
            }
        };

        public static SyntaxDefinition CancelSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Cancel,
            }
        };

        public static SyntaxDefinition ResetSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Reset,
            }
        };
        public static SyntaxDefinition ResponseSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Response,
                SpeechParameterType.Text
            }
        };

        public static SyntaxDefinition RepeatSpeechCommandSyntax { get; } = new SyntaxDefinition()
        {
            SpeechTypesSyntax = new List<Enum>()
            {
                SpeechCommandType.Repeat,
            }
        };
    }
}