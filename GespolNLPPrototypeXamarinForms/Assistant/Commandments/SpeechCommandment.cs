using System.Collections.Generic;
using System.Linq;
using XamarinFormsAssistant.Assistant.Enums;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Assistant.Syntax;

namespace XamarinFormsAssistant.Assistant.Commandments
{
    public class SpeechCommandment
    {
        public SpeechCommand MainCommand { get; set; }
        public SpeechParameter MainParameter { get; set; }
        public List<SpeechParameter> AdditionalParameters { get; set; } = new List<SpeechParameter>();

        public bool IsSyntaxValid()
        {
            return CommandmentSyntaxDefinitions.IsSyntaxValid(this);
        }

        public List<SpeechCommandment> FindSubCommandments()
        {
            var resultSubCommandments = new List<SpeechCommandment>();
            if (MainCommand == null) return resultSubCommandments;

            var correctSyntaxDefinition = CommandmentSyntaxDefinitions.SyntaxDefinitions[MainCommand.SpeechCommandType];

            switch (correctSyntaxDefinition.SpeechTypesSyntax.Count)
            {
                case 1:
                    if (MainParameter == null) return null;
                    if (MainParameter.SpeechParameterType == SpeechParameterType.Property && AdditionalParameters.Any())
                    {
                        var subcommandment = new SpeechCommandment()
                        {
                            MainCommand = new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update},
                            MainParameter = MainParameter,
                            AdditionalParameters = AdditionalParameters
                        };
                        resultSubCommandments.Add(subcommandment.ClearSyntax());
                    }
                    break;
                case 2:
                    if (AdditionalParameters == null || !AdditionalParameters.Any()) return null;
                    // ReSharper disable once PossibleNullReferenceException
                    if (AdditionalParameters.FirstOrDefault().SpeechParameterType == SpeechParameterType.Property && AdditionalParameters.Count > 1)
                    {
                        var subcommandment = new SpeechCommandment()
                        {
                            MainCommand = new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update},
                            MainParameter = AdditionalParameters.FirstOrDefault(),
                            AdditionalParameters = AdditionalParameters.GetRange(1, AdditionalParameters.Count - 1)
                        };
                        resultSubCommandments.Add(subcommandment.ClearSyntax());
                    }
                    break;
                default:
                    if (AdditionalParameters == null || AdditionalParameters.Count < 3) return null;
                    var nextPropertySpeechParameter =
                        AdditionalParameters.Skip(1)
                            .ToList()
                            .Find(parameter => parameter.SpeechParameterType == SpeechParameterType.Property);
                    if (nextPropertySpeechParameter != null)
                    {
                        var subcommandment = new SpeechCommandment()
                        {
                            MainCommand = new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update},
                            MainParameter = nextPropertySpeechParameter,
                            AdditionalParameters = AdditionalParameters.GetRange(AdditionalParameters.IndexOf(nextPropertySpeechParameter) + 1, AdditionalParameters.Count - AdditionalParameters.IndexOf(nextPropertySpeechParameter) - 1)
                        };
                        resultSubCommandments.Add(subcommandment.ClearSyntax());
                    }
                    break;
            }

            return resultSubCommandments;
        }

        public SpeechCommandment ClearSyntax()
        {
            if (MainCommand == null) return this;
            var correctSyntaxDefinition = CommandmentSyntaxDefinitions.SyntaxDefinitions[MainCommand.SpeechCommandType];

            if (correctSyntaxDefinition.SpeechTypesSyntax.Count == 1)
            {
                MainParameter = null;
                AdditionalParameters = null;
            }
            else if (correctSyntaxDefinition.SpeechTypesSyntax.Count == 2)
            {
                AdditionalParameters = null;
            }
            else
            {
                var propertyParameter =
                    AdditionalParameters.FirstOrDefault(
                        parameter => parameter.SpeechParameterType == SpeechParameterType.Property);
                if (propertyParameter != null)
                {
                    AdditionalParameters.RemoveRange(AdditionalParameters.IndexOf(propertyParameter), AdditionalParameters.Count - AdditionalParameters.IndexOf(propertyParameter));
                }
            }
            return this;
        }
    }
}