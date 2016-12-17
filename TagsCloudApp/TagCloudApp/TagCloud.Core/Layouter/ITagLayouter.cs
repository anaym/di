using Utility.Geometry;

namespace TagCloud.Core.Layouter
{
    public interface ITagLayouter
    {
        Rectangle PutNextTag(string tag, int frequence);
    }
}