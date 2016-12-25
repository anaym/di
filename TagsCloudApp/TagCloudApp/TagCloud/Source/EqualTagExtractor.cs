using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class EqualTagExtractor : ITagExtractor
    {
        public Result<string> ExtractTag(string word)
        {
            return Results.Success(word);
        }
    }
}