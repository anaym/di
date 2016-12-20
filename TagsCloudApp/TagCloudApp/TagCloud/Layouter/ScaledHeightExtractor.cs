using TagCloud.Core.Layouter;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Layouter
{
    public class ScaledHeightExtractor : IHeightExtractor
    {
        private readonly int scale;

        public ScaledHeightExtractor(int scale = 16)
        {
            this.scale = scale;
        }

        public Result<int> ExtractHeight(Result<int> frequence)
        {
            return frequence.Select(f => f*scale);
        }
    }
}