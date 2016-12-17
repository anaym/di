using System;
using System.Drawing;
using TagCloud.Core.Layouter;
using Utility.Geometry.Extensions;
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

        public Size ExtractSize(string word, int frequency)
        {
            var height = heightExtractor.ExtractHeight(frequency);
            return graphic.MeasureString(word, new Font(FontFamily.GenericMonospace, height)).ToGeometrySize();
        }

        public void Dispose()
        {
            graphic.Dispose();
            bitmap.Dispose();
        }
    }
}