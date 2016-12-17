using System.Collections.Generic;
using System.Drawing;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloud.Core.Renderer
{
    public interface ITagCloudRenderer
    {
        Rectangle GetCoverageRectangle(IReadOnlyDictionary<string, Rectangle> tags);
        void Render(Graphics graphics, IReadOnlyDictionary<string, Rectangle> tags);
    }
}