﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using Utility.Geometry;
using Utility.Geometry.Extensions;
using Rectangle = Utility.Geometry.Rectangle;
using Size = Utility.Geometry.Size;

namespace TagCloudApp.TagCloudRender
{
    public class TagCloudRenderer : ITagCloudRenderer
    {
        private readonly StringFormat stringFormat;
        private readonly RenderSettings settings;

        public TagCloudRenderer(RenderSettings settings)
        {
            this.settings = settings;
            stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        public Rectangle GetCoverageRectangle(IReadOnlyDictionary<string, Rectangle> tags)
        {
            return tags.Values.CoveringRectangle() * settings.Scale;
        }

        public void Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags)
        {
            var transform = new VectorCoordinateSystemConverter(GetCoverageRectangle(tags));
            var s = settings.Scale;
            if (settings.ShowRectangles)
            {
                foreach (var rectangle in tags.Values)
                {
                    var rectF = transform.Transform(rectangle*s);
                    graphics.FillRectangle(new SolidBrush(rectangle.Size.ToColor()), rectF);
                    graphics.DrawRectangle(new Pen(Color.GreenYellow), rectF.X, rectF.Y, rectF.Width, rectF.Height);

                }
            }
            var rnd = new Random();
            var textBrushes = new[] {new SolidBrush(settings.TextColor),};
            foreach (var tag in tags)
            {
                var rectF = transform.Transform(tag.Value*s);
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                var goodFont = FindFont(graphics, tag.Key, rectF.Size, new Font(FontFamily.GenericMonospace, 128));
                var textBrush = textBrushes[rnd.Next(textBrushes.Length)];
                graphics.DrawString(tag.Key, goodFont, textBrush, rectF, stringFormat);
            }
        }

        private static Font FindFont(Graphics g, string str, SizeF room, Font preferedFont)
        {
            SizeF realSize = g.MeasureString(str, preferedFont);
            float heightScaleRatio = room.Height / realSize.Height;
            float widthScaleRatio = room.Width / realSize.Width;
            float scaleRatio = (heightScaleRatio < widthScaleRatio) ? heightScaleRatio : widthScaleRatio;
            float scaleFontSize = preferedFont.Size * scaleRatio;
            return new Font(preferedFont.FontFamily, scaleFontSize);
        }
    }
}