using Utility.RailwayExceptions;
using Size = Utility.Geometry.Size;

namespace TagCloud.Core.Layouter
{
    public interface ISizeExtractor
    {
        Result<Size> ExtractSize(string word, int frequency);
    }
}