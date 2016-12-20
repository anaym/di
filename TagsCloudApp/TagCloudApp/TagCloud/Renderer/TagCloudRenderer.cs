using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using TagCloud.Core.Renderer;
using TagCloud.Settings;
using Utility.Geometry;
using Utility.Geometry.Extensions;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Renderer
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

        public Result<Rectangle> GetCoverageRectangle(IReadOnlyDictionary<Result<string>, Result<Rectangle>> tags)
        {
            return Result.Success(tags.Values.CoveringRectangle() * settings.Scale);
        }

        public Result<None> Render(Result<Graphics> graphics, IReadOnlyDictionary<Result<string>, Result<Rectangle>> tags)
        {
            if (graphics.IsFail) return Result.Fail(graphics.Exception).RefineException("Render error");
            var coverigRectangle = GetCoverageRectangle(tags);
            if (coverigRectangle.IsFail) return Result.Fail(coverigRectangle.Exception).RefineException("Render error");
            var transform = new VectorCoordinateSystemConverter(coverigRectangle.GetValueOrThrow());
            var scale = settings.Scale;
            var graph = graphics.GetValueOrThrow();
            if (settings.ShowRectangles)
            {
                foreach (var rectangle in tags.Values.GetSuccesful())
                {
                    var rectF = transform.Transform(rectangle*scale);
                    graph.FillRectangle(new SolidBrush(rectangle.Size.ToColor()), rectF);
                    graph.DrawRectangle(new Pen(Color.GreenYellow), rectF.X, rectF.Y, rectF.Width, rectF.Height);

                }
            }
            var rnd = new Random();
            var textBrushes = settings.TextColors.Select(c => new SolidBrush(c)).ToList();
            var font = new Font(new FontFamily(settings.Font), 128);
            foreach (var tag in tags.GetSuccesful())
            {
                var rectF = transform.Transform(tag.Value*scale);
                graph.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                var goodFont = FindFont(graph, tag.Key, rectF.Size, font);
                var textBrush = textBrushes[rnd.Next(textBrushes.Count)];
                graph.DrawString(tag.Key, goodFont, textBrush, rectF, stringFormat);
            }
            return Result.Success();
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