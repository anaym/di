using System;
using System.Collections.Generic;
using System.Linq;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace Utility.Geometry.Extensions
{
    public static class CoveringRectangleExtension
    {
        public static Rectangle CoveringRectangle(this IEnumerable<Result<Rectangle>> rectangles)
        {
            var minX = int.MaxValue;
            var maxX = int.MinValue;
            var minY = int.MaxValue;
            var maxY = int.MinValue;
            var exists = false;
            foreach (var rectangle in rectangles.GetSuccesful())
            {
                exists = true;
                minX = Math.Min(rectangle.Left, minX);
                minY = Math.Min(rectangle.Bottom, minY);

                maxX = Math.Max(rectangle.Right, maxX);
                maxY = Math.Max(rectangle.Top, maxY);
            }
            return !exists ? Rectangle.FromCentre(Vector.Zero, Size.Empty) : new Rectangle(maxX, maxY, minX, minY);
        }
    }
}