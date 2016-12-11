using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudApp.IO
{
    public class TestPngImageDestination : IImageDestination
    {
        public void Save(Bitmap bitmap)
        {
            bitmap.Save("test.png", ImageFormat.Png);
        }
    }
}