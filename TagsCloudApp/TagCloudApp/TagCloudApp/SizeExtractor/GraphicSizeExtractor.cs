using System;
using System.Drawing;
using TagCloudApp.HeightExtractor;
using Utility.Geometry.Extensions;
using Size = Utility.Geometry.Size;

namespace TagCloudApp.SizeExtractor
{
    class GraphicSizeExtractor : ISizeExtractor, IDisposable
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

        public Size ExtractSize(string word, int frequience)
        {
            var height = heightExtractor.ExtractHeight(frequience);
            return graphic.MeasureString(word, new Font(FontFamily.GenericMonospace, height)).ToGeometrySize();
        }

        public void Dispose()
        {
            graphic.Dispose();
            bitmap.Dispose();
        }
    }
}