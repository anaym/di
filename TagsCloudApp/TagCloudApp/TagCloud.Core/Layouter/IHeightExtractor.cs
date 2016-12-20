using Utility.RailwayExceptions;

namespace TagCloud.Core.Layouter
{
    public interface IHeightExtractor
    {
        Result<int> ExtractHeight(Result<int> frequence);
    }
}