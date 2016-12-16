using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    class AllTagFilter : ITagFilter
    {
        public bool IsCollectableTag(string tag)
        {
            return true;
        }
    }
}