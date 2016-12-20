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

        public Result<Size> ExtractSize(Result<string> word, Result<int> frequency)
        {
            var height = heightExtractor.ExtractHeight(frequency);
            return word.And(height, (w, h) => graphic.MeasureString(w, new Font(FontFamily.GenericMonospace, h)).ToGeometrySize());
        }

        public void Dispose()
        {
            graphic.Dispose();
            bitmap.Dispose();
        }
    }
}