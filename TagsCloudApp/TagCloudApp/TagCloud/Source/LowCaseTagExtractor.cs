using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public Result<string> ExtractTag(string word)
        {
            return Result.Of(() => word.ToLowerInvariant());
        }
    }
}