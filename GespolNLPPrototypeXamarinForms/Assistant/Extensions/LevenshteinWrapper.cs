using System;
using System.Collections.Generic;
using System.Linq;
using MinimumEditDistance;
using XamarinFormsAssistant.Assistant.Helper;

namespace XamarinFormsAssistant.Assistant.Extensions
{
    public static class LevenshteinWrapper
    {
        public static float LevenstheinContains(string possibleSubstring, string text, float levenstheinThreshold)
        {
            text = DiacriticRemoverHelper.RemoveDiacritics(text).ReplaceAllSymbols(" ");
            possibleSubstring = DiacriticRemoverHelper.RemoveDiacritics(possibleSubstring).ReplaceAllSymbols(" ");

            var splittedText = text.Split(' ');
            var possibleSubstringSplittedText = possibleSubstring.Split(' ');

            var relativeScoresDictionary = new Dictionary<Tuple<string,string>,float>();

            foreach (var possibleSplittedTextPortion in possibleSubstringSplittedText)
            {
                int intParse;
                if (possibleSplittedTextPortion.Length <= 3 && !int.TryParse(possibleSplittedTextPortion, out intParse))
                    continue;
                foreach (var textPortion in splittedText)
                {
                    if (textPortion.Length <= 3 && !int.TryParse(textPortion, out intParse)) continue;
                    var score = Levenshtein.CalculateDistance(textPortion.ToUpper(),
                        possibleSplittedTextPortion.ToUpper(), 1);
                    var relativeScoreValue = (float) score/(float) textPortion.Length;
                    var relativeScoreKey = new Tuple<string, string>(possibleSplittedTextPortion, textPortion);
                    if(!relativeScoresDictionary.ContainsKey(relativeScoreKey)) relativeScoresDictionary.Add(relativeScoreKey, relativeScoreValue);
                }
            }

            var splittedKeyValues = relativeScoresDictionary.Split();
            if (!splittedKeyValues.Any()) return 0.0f;
            return splittedKeyValues.Sum(splittedKeyValue => 1 - splittedKeyValue.Min(f => f)) / splittedKeyValues.Count;
        }

        public static IList<T> GetLevenstheinContainsList<T>(string possibleSubstring, IList<T> entityList,
            float levenstheinThreshold)
        {
            var candidateProbabilityTuple = new List<Tuple<T, float>>();
            foreach (var entity in entityList)
            {
                var similitudeProbability = LevenstheinContains(possibleSubstring, entity.ToString(),
                    levenstheinThreshold);
                if (similitudeProbability > 0.0f)
                {
                    candidateProbabilityTuple.Add(new Tuple<T, float>(entity, similitudeProbability));
                }
            }
            return candidateProbabilityTuple.FilterCandidates();
        }
    }

    public static class CandidateProbabilityTupleExtension
    {
        public static IList<T> FilterCandidates<T>(this List<Tuple<T, float>> candidateProbabilityTuple)
        {
            if(!candidateProbabilityTuple.Any())
                return new List<T>();

            var maxSimilitudeProbability = candidateProbabilityTuple.Max(tuple => tuple.Item2);
            var result =
                candidateProbabilityTuple.FindAll(tuple => Math.Abs(tuple.Item2 - maxSimilitudeProbability) < 0.15);

            //return result.FirstOrDefault()?.Item2 == 1.0f
            //    ? new List<T>() { result.FirstOrDefault().Item1 }
            //    : new List<T>(result.OrderByDescending(l => l.Item2).Select(tuple => tuple.Item1));

            return new List<T>(result.OrderByDescending(l => l.Item2).Select(tuple => tuple.Item1));
        }
    }

    public static class VectorialSplitExtension
    {
        public static List<List<float>> Split(this Dictionary<Tuple<string, string>, float> dictionary)
        {
            var currentItem = string.Empty;
            List<float> currentList = null;
            var result = new List<List<float>>();
            for (var i = 0; i < dictionary.Keys.Count; i++)
            {
                if (dictionary.Keys.ElementAt(i).Item1.Equals(currentItem))
                {
                    currentList.Add(dictionary[dictionary.Keys.ElementAt(i)]);
                }
                else
                {
                    currentItem = dictionary.Keys.ElementAt(i).Item1;
                    currentList = new List<float> { dictionary[dictionary.Keys.ElementAt(i)] };
                    result.Add(currentList);
                }
            }

            return result;
        }
    }
}