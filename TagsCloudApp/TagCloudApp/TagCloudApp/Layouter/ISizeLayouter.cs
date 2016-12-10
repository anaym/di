using System.Collections.Generic;
using System.Linq;
using Utility.Geometry;

namespace TagCloudApp.Layouter
{
    public interface ISizeLayouter
    {
        Rectangle PutNextSize(Size size);
    }

    public static class SizeLayouterHelper
    {
        public static IEnumerable<Rectangle> PutManySizes(this ISizeLayouter layouter, IEnumerable<Size> sizes)
        {
            return sizes.Select(layouter.PutNextSize);
        }
    }
}