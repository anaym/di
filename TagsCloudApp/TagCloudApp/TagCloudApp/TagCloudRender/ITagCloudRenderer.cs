using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Size = Utility.Geometry.Size;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloudApp.TagCloudRender
{
    public interface ITagCloudRenderer
    {
        Rectangle GetCoverageRectangle(IReadOnlyDictionary<string, Rectangle> tags);
        void Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags);
    }

    public static class TagCloudRendererHelper
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