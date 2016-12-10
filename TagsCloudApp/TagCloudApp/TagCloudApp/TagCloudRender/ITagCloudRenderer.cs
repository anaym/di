using System.Collections.Generic;
using System.Drawing;
using Size = Utility.Geometry.Size;

namespace TagCloudApp.TagCloudRender
{
    public interface ITagCloudRenderer
    {
        Size GetSize(IReadOnlyDictionary<string, Rectangle> tags);
        void Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags);
    }

    public static class TagCloudRendererHelper
    {
        public static Bitmap Render(this ITagCloudRenderer renderer, IReadOnlyDictionary<string, Rectangle> tags)
        {
            var size = renderer.GetSize(tags);
            var bitmap = new Bitmap(size.Width, size.Height);
            renderer.Render(Graphics.FromImage(bitmap), tags);
            return bitmap;
        }
    }
}