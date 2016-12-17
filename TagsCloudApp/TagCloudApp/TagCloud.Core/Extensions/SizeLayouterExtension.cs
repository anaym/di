﻿using System.Collections.Generic;
using System.Linq;
using Utility.Geometry;

namespace TagCloud.Core.Layouter.Extensions
{
    public static class SizeLayouterExtension
    {
        public static IEnumerable<Rectangle> PutManySizes(this ISizeLayouter layouter, IEnumerable<Size> sizes)
        {
            return sizes.Select(layouter.PutNextSize);
        }
    }
}