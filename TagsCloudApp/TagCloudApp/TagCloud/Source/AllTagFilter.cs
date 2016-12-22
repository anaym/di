using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class AllTagFilter : ITagFilter
    {
        public Result<bool> IsCollectableTag(string tag)
        {
            return Result.Success(true);
        }
    }
}