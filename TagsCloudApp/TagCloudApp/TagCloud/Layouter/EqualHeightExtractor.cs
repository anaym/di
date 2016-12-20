using TagCloud.Core.Layouter;
using Utility.RailwayExceptions;

namespace TagCloud.Layouter
{
    public class EqualHeightExtractor : IHeightExtractor
    {
        public Result<int> ExtractHeight(Result<int> frequence)
        {
            return frequence;
        }
    }
}