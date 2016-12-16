using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using TagCloud.Core.Renderer;
using Utility.Geometry;
using Utility.Geometry.Extensions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloudApp.Renderer
{
    public class TagCloudRenderer : ITagCloudRenderer
    {
        private readonly StringFormat stringFormat;
        private readonly RendererSettings settings;

        public TagCloudRenderer(RendererSettings settings)
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
            var scale = settings.Scale;
            if (settings.ShowRectangles)
            {
                foreach (var rectangle in tags.Values)
                {
                    var rectF = transform.Transform(rectangle*scale);
                    graphics.FillRectangle(new SolidBrush(rectangle.Size.ToColor()), rectF);
                    graphics.DrawRectangle(new Pen(Color.GreenYellow), rectF.X, rectF.Y, rectF.Width, rectF.Height);

                }
            }
            var rnd = new Random();
            var textBrushes = settings.TextColors.Select(c => new SolidBrush(c)).ToList();
            var font = new Font(new FontFamily(settings.Font), 128);
            foreach (var tag in tags)
            {
                var rectF = transform.Transform(tag.Value*scale);
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                var goodFont = FindFont(graphics, tag.Key, rectF.Size, font);
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