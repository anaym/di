using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public Result<string> ExtractTag(string word)
        {
            return Results.Of(() => word.ToLowerInvariant());
        }
    }
}