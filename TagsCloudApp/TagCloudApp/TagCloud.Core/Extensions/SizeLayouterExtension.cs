using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Layouter;
using Utility.Geometry;
using Utility.RailwayExceptions;

namespace TagCloud.Core.Extensions
{
    public static class SizeLayouterExtension
    {
        public static IEnumerable<Result<Rectangle>> PutManySizes(this ISizeLayouter layouter, IEnumerable<Size> sizes)
        {
            return sizes.Select(layouter.PutNextSize);
        }
    }
}