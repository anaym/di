using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Renderer
{
    public static class TagCloudRendererExtension
    {
        public static Bitmap Render(this ITagCloudRenderer renderer, IReadOnlyDictionary<string, Rectangle> tags)
        {
            var size = renderer.GetCoverageRectangle(tags);
            if (size.Size.Width == 0 || size.Size.Height == 0)
                return null;
            var bitmap = new Bitmap(size.Size.Width, size.Size.Height, PixelFormat.Format24bppRgb);
            renderer.Render(Graphics.FromImage(bitmap), tags);
            return bitmap;
        }
    }
}