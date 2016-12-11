namespace TagCloudApp.WordToTag
{
    class AllTagFilter : ITagFilter
    {
        public bool IsCollectedTag(string tag)
        {
            return true;
        }
    }
}