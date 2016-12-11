using System.Drawing;

namespace TagCloudApp
{
    public interface ITagLayoutTask
    {
        Bitmap Solve();
    }
}