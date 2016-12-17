using TagCloud.Core.Source;

namespace TagCloud.Source
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word.ToLowerInvariant();
        }
    }
}