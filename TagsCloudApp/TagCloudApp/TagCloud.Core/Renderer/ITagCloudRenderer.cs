using System.Collections.Generic;
using System.Drawing;
using Utility.RailwayExceptions;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Renderer
{
    public interface ITagCloudRenderer
    {
        Result<Rectangle> GetCoverageRectangle(IReadOnlyDictionary<Result<string>, Result<Rectangle>> tags);
        Result<None> Render(Result<Graphics> graphics, IReadOnlyDictionary<Result<string>, Result<Rectangle>> tags);
    }
}