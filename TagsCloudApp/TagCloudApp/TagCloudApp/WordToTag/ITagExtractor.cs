using System.Collections.Generic;

namespace TagCloudApp.WordToTag
{
    public interface ITagExtractor
    {
        string ExtractTag(string word);
    }
}