using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Core.Renderer;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Extensions
{
    public static class TagCloudRendererExtension
    {
        public static Result<Bitmap> Render(this ITagCloudRenderer renderer, IReadOnlyDictionary<Result<string>, Result<Rectangle>> tags)
        {
            var bitmap = renderer
                .GetCoverageRectangle(tags)
                .Select(r => r.Size)
                .Validate(s => s.Width != 0 && s.Height != 0, "Too low image")
                .Select(s => new Bitmap(s.Width, s.Height, PixelFormat.Format24bppRgb));

            return bitmap.Execute(b => renderer.Render(Result.Success(Graphics.FromImage(b)), tags));
        }
    }
}