using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word.ToLowerInvariant();
        }
    }
}