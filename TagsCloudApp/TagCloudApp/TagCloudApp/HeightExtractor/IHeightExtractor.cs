using System.Drawing;
using Utility.Geometry.Extensions;
using Size = Utility.Geometry.Size;

namespace TagCloudApp.SizeExtractor
{
    public interface IHeightExtractor
    {
        int ExtractHeight(int frequience);
    }
}