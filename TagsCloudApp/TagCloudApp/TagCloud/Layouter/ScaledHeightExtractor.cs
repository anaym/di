using TagCloud.Core.Layouter;
using Utility.RailwayExceptions;

namespace TagCloud.Layouter
{
    public class ScaledHeightExtractor : IHeightExtractor
    {
        private readonly int scale;

        public ScaledHeightExtractor(int scale = 16)
        {
            this.scale = scale;
        }

        public Result<int> ExtractHeight(int frequence)
        {
            return Results.Of(() => frequence*scale);
        }
    }
}