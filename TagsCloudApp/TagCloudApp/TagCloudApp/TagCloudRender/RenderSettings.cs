using System.Drawing;

namespace TagCloudApp.TagCloudRender
{
    public class RenderSettings
    {
        public Color TextColor { get; set; } = Color.Red;
        public int Scale { get; set; } = 1;
        public bool ShowRectangles { get; set; } = false;
    }
}