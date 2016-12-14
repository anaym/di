namespace TagCloudApp.WordToTag
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public string ExtractTag(string word)
        {
            return word.ToLowerInvariant();
        }
    }
}