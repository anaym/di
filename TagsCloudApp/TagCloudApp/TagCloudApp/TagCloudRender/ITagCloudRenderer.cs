using System.Collections.Generic;
using System.Drawing;
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
            var bitmap = new Bitmap(size.Size.Width, size.Size.Height);
            renderer.Render(Graphics.FromImage(bitmap), tags);
            return bitmap;
        }
    }
}