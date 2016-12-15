using System.Collections.Generic;
using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public class TestWordSource : IWordsSource
    {
        public IEnumerable<string> GetWords()
        {
            return new[] {"a", "b", "a", "c", "d", "D"};
        }
    }
}