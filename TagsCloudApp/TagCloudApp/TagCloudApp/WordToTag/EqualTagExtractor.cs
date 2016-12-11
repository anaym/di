using System.Collections.Generic;

namespace TagCloudApp.WordToTag
{
    public class EqualTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word;
        }
    }
}