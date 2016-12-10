using System.Collections.Generic;

namespace TagCloudApp.WordToTag
{
    public class TestWordsToTag : IWordsToTag
    {
        public IEnumerable<string> ToTags(IEnumerable<string> words)
        {
            return words;
        }
    }
}