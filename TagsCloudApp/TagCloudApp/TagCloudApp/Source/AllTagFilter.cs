using TagCloud.Core.Source;

namespace TagCloudApp.Source
{
    class AllTagFilter : ITagFilter
    {
        public bool IsCollectedTag(string tag)
        {
            return true;
        }
    }
}