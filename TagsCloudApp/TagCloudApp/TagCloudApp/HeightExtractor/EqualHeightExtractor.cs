using TagCloudApp.SizeExtractor;
using Utility.Geometry;

namespace TagCloudApp.HeightExtractor
{
    public class EqualHeightExtractor : IHeightExtractor
    {
        public int ExtractHeight(int frequience)
        {
            return frequience;
        }
    }
}