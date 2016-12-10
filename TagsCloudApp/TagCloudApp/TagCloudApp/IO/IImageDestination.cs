using System.Drawing;

namespace TagCloudApp.IO
{
    public interface IImageDestination
    {
        void Save(Bitmap bitmap);
    }
}