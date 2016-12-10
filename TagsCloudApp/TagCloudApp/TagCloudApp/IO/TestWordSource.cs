using System.Collections.Generic;

namespace TagCloudApp.IO
{
    public class TestWordSource : IWordsSource
    {
        public IEnumerable<string> GetWords()
        {
            return new[] {"a", "b", "a", "c", "d"};
        }
    }
}