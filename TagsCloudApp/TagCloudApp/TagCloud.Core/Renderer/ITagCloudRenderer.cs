using System.Collections.Generic;
using System.Drawing;
using Utility.RailwayExceptions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Renderer
{
    public interface ITagCloudRenderer
    {
        Result<Rectangle> GetCoverageRectangle(Result<IReadOnlyDictionary<string, Rectangle>> tags);
        Result<None> Render(Result<Graphics> graphics, Result<IReadOnlyDictionary<string, Rectangle>> tags);
    }
}