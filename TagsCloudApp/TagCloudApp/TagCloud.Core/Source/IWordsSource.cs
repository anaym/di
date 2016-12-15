using System.Collections.Generic;

namespace TagCloud.Core.Source
{
    public interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}