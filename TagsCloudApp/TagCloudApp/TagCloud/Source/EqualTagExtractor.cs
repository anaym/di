using TagCloud.Core.Source;

namespace TagCloud.Source
{
    public class EqualTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word;
        }
    }
}