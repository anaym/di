using TagCloud.Core.Layouter;
using Utility.RailwayExceptions;

namespace TagCloud.Layouter
{
    public class EqualHeightExtractor : IHeightExtractor
    {
        public Result<int> ExtractHeight(int frequence)
        {
            return Results.Success(frequence);
        }
    }
}