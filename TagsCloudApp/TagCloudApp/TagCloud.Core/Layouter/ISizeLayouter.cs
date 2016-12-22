using Utility.Geometry;
using Utility.RailwayExceptions;

namespace TagCloud.Core.Layouter
{
    public interface ISizeLayouter
    {
        Result<Rectangle> PutNextSize(Size size);
    }
}