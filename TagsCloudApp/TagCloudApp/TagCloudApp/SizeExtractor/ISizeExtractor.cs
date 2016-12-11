using Size = Utility.Geometry.Size;

namespace TagCloudApp.SizeExtractor
{
    public interface ISizeExtractor
    {
        Size ExtractSize(string word, int frequence);
    }
}