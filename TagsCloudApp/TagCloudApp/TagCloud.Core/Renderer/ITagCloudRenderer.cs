using System.Collections.Generic;
using System.Drawing;
using Utility.RailwayExceptions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Renderer
{
    public interface ITagCloudRenderer
    {
        Result<Rectangle> GetCoverageRectangle(IReadOnlyDictionary<string, Rectangle> tags);
        Result<None> Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags);
    }
}