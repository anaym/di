using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public class EqualTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word;
        }
    }
}