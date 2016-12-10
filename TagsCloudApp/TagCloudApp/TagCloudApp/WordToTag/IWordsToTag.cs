using System.Collections.Generic;

namespace TagCloudApp.WordToTag
{
    public interface IWordsToTag
    {
        IEnumerable<string> ToTags(IEnumerable<string> words);
    }
}