using System;
using System.Drawing;
using TagCloud.Core.Layouter;
using Utility.Geometry.Extensions;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;
using Size = Utility.Geometry.Size;

namespace TagCloud.Layouter
{
    public class GraphicSizeExtractor : ISizeExtractor, IDisposable
    {
        private readonly IHeightExtractor heightExtractor;
        private readonly Graphics graphic;
        private readonly Bitmap bitmap;

        public GraphicSizeExtractor(IHeightExtractor heightExtractor)
        {
            bitmap = new Bitmap(1, 1);
            graphic = Graphics.FromImage(bitmap);
            this.heightExtractor = heightExtractor;
        }

        public Result<Size> ExtractSize(string word, int frequency)
        {
            return heightExtractor
                .ExtractHeight(frequency)
                .Select(h => new Font(FontFamily.GenericMonospace, h))
                .Select(font => graphic.MeasureString(word, font))
                .Select(s => s.ToGeometrySize())
                .RefineError("Size extract error");
        }

        public void Dispose()
        {
            graphic.Dispose();
            bitmap.Dispose();
        }
    }
}