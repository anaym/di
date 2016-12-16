using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    public interface IFileWordsSource : IWordsSource
    {
        bool IsCanRead();
    }
}