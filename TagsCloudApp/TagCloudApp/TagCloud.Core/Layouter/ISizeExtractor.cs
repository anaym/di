using Utility.RailwayExceptions;
using Size = Utility.Geometry.Size;

namespace TagCloud.Core.Layouter
{
    public interface ISizeExtractor
    {
        Result<Size> ExtractSize(Result<string> word, Result<int> frequency);
    }
}