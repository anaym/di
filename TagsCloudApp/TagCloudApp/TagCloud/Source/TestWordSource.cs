using System.Collections.Generic;
using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class TestWordSource : IWordsSource
    {
        public Result<IEnumerable<string>> GetWords()
        {
            return Result.Success<IEnumerable<string>>(new[] {"a", "b", "a", "c", "d", "D"});
        }
    }
}