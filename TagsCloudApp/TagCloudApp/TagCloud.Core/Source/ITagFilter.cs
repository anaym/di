namespace TagCloud.Core.Source
{
    public interface ITagFilter
    {
        bool IsCollectedTag(string tag);
    }
}