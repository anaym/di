using System;
using System.Drawing;
using Utility.Geometry.Extensions;
using Size = Utility.Geometry.Size;

namespace TagCloudApp.SizeExtractor
{
    public interface ISizeExtractor
    {
        Size ExtractSize(string word, int frequence);
    }

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

        public Size ExtractSize(string word, int height)
        {
            return graphic.MeasureString(word, new Font(FontFamily.GenericMonospace, height)).ToGeometrySize();
        }

        public void Dispose()
        {
            graphic.Dispose();
            bitmap.Dispose();
        }
    }
}