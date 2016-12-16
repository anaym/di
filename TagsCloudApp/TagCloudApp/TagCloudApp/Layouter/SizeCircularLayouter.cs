﻿using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Layouter;
using Utility;
using Utility.Geometry;

namespace TagCloudApp.Layouter
{
    public class SizeCircularLayouter : ISizeLayouter
    {
        public readonly Vector Centre;
        public readonly Vector Extension;

        private readonly List<Rectangle> rectangles;
        private readonly HashSet<Vector> spots;
        private Vector averageVector;

        public SizeCircularLayouter(LayouterSettings settings) : this(settings.Width, settings.Height)
        { }

        public SizeCircularLayouter(int w, int h)
        {
            Centre = new Vector(0, 0);
            Extension = new Vector(w, h);
            rectangles = new List<Rectangle>();
            averageVector = Vector.Zero;
            spots = new HashSet<Vector>();
        }

        public Rectangle PutNextSize(Size rectangleSize)
        {
            var rect = Rectangle.FromCentre(Centre, rectangleSize);
            if (rectangles.Any())
            {
                rect = TryInsert(rectangleSize);
            }

            averageVector = rect.Centre + rect.LeftBottom + rect.LeftTop + rect.RightBottom + rect.RightTop - Centre * 5 + averageVector;
            rectangles.Add(rect);

            spots.Add(new Vector(rect.Left, rect.Centre.Y));
            spots.Add(new Vector(rect.Right, rect.Centre.Y));
            spots.Add(new Vector(rect.Centre.X, rect.Top));
            spots.Add(new Vector(rect.Centre.X, rect.Bottom));
            spots.Add(rect.LeftBottom);
            spots.Add(rect.RightTop);
            spots.Add(rect.LeftTop);
            spots.Add(rect.RightBottom);

            return rect;
        }
        private Rectangle TryInsert(Size size)
        {
            var rect = spots
                .SelectMany(w => new[]
                {
                    Rectangle.FromLeftBottom(w, size),
                    Rectangle.FromRightBottom(w, size),
                    Rectangle.FromLeftTop(w, size),
                    Rectangle.FromRightTop(w, size),
                })
                .Where(r => !IsIntersected(r)).ToList()
                .MinOrDefault(Aberration);
            return rect;
        }

        private int Aberration(Rectangle rect)
        {
            var newVector = rect.Centre + rect.LeftBottom + rect.LeftTop + rect.RightBottom + rect.RightTop - Centre * 5 + averageVector;
            return Math.Abs(newVector.X / Extension.X) + Math.Abs(newVector.Y / Extension.Y);
        }

        private bool IsIntersected(Rectangle subject)
        {
            return rectangles.Any(r => r.IsIntersected(subject, false));
        }
    }
}
