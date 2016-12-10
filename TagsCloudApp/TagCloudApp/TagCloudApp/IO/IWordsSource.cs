using System.Collections.Generic;

namespace TagCloudApp.IO
{
    public interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}