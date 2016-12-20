using TagCloud.Core.Source;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Source
{
    public class LowCaseTagExtractor : ITagExtractor
    {
        public Result<string> ExtractTag(Result<string> word)
        {
            return word.Select(w => w.ToLowerInvariant());
        }
    }
}