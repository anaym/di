using Size = Utility.Geometry.Size;

namespace TagCloud.Core.Layouter
{
    public interface ISizeExtractor
    {
        Size ExtractSize(string word, int frequence);
    }
}