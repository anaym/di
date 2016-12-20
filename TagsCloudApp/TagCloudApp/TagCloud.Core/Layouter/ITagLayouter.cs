using Utility.Geometry;
using Utility.RailwayExceptions;

namespace TagCloud.Core.Layouter
{
    public interface ITagLayouter
    {
        Result<Rectangle> PutNextTag(Result<string> tag, Result<int> frequence);
    }
}