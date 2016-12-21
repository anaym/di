using Utility.Geometry;
using Utility.RailwayExceptions;

namespace TagCloud.Core.Layouter
{
    public interface ITagLayouter
    {
        Result<Rectangle> PutNextTag(string tag, int frequence);
    }
}