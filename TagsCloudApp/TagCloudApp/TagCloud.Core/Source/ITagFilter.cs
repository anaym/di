namespace TagCloud.Core.Source
{
    public interface ITagFilter
    {
        bool IsCollectableTag(string tag);
    }
}