namespace TagCloud.Core.Source
{
    public interface IFileWordsSource : IWordsSource
    {
        bool IsCanRead();
    }
}