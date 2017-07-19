using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Fragments;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Handlers
{
    public abstract class SearchHandler<T> : ISearchHandler where T : EntityBase
    {
        public abstract IList<T> Search(List<SpeechParameter> additionalParameters);
    }
}