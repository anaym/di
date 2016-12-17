using Utility.Geometry;

namespace TagCloud.Core.Layouter
{
    public interface ISizeLayouter
    {
        Rectangle PutNextSize(Size size);
    }
}