namespace TagCloudApp.WordToTag
{
    public interface ITagFilter
    {
        bool IsCollectedTag(string tag);
    }
}