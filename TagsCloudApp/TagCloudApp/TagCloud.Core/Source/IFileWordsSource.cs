using Utility.RailwayExceptions;

namespace TagCloud.Core.Source
{
    public interface IFileWordsSource : IWordsSource
    {
        Result<bool> IsCanRead();
    }
}