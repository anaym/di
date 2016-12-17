using TagCloud.Core.Source;

namespace TagCloud.Source
{
    public class AllTagFilter : ITagFilter
    {
        public bool IsCollectableTag(string tag)
        {
            return true;
        }
    }
}