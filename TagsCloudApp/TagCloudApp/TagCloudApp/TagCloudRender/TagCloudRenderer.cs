using System;
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
        private readonly List<Brush> textBrushes;
        private readonly bool showRectangles;
        private readonly StringFormat stringFormat;

        public void AddColor(Color color) => textBrushes.Add(new SolidBrush(color));
        public void AddManyColors(params Color[] colors) => textBrushes.AddRange(colors.Select(c => new SolidBrush(c)));

        public TagCloudRenderer(bool showRectangles = false)
        {
            textBrushes = new List<Brush> { new SolidBrush(Color.DarkRed) };
            this.showRectangles = showRectangles;
            stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        public Rectangle GetCoverageRectangle(IReadOnlyDictionary<string, Rectangle> tags)
        {
            return tags.Values.CoveringRectangle();
        }

        public void Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags)
        {
            var transform = new VectorCoordinateSystemConverter(GetCoverageRectangle(tags));
            if (showRectangles)
            {
                foreach (var rectangle in tags.Values)
                {
                    var rectF = transform.Transform(rectangle);
                    graphics.FillRectangle(new SolidBrush(rectangle.Size.ToColor()), rectF);
                    graphics.DrawRectangle(new Pen(Color.GreenYellow), rectF.X, rectF.Y, rectF.Width, rectF.Height);

                }
            }
            var rnd = new Random();
            foreach (var tag in tags)
            {
                var rectF = transform.Transform(tag.Value);
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                var goodFont = FindFont(graphics, tag.Key, rectF.Size, new Font(FontFamily.GenericMonospace, 128));
                var textBrush = textBrushes[rnd.Next(textBrushes.Count)];
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