using Utility.RailwayExceptions;

namespace TagCloud.Core.Source
{
    public interface ITagFilter
    {
        Result<bool> IsCollectableTag(string tag);
    }
}