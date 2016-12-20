using System.Collections.Generic;
using Utility.RailwayExceptions;

namespace TagCloud.Core.Source
{
    public interface IWordsSource
    {
        Result<IEnumerable<string>> GetWords();
    }
}