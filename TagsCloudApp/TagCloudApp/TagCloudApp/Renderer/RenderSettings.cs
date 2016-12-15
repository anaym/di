using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApp.Renderer
{
    public class RenderSettings
    {
        public List<Color> TextColors { get; set; } = new List<Color> {Color.Red};
        public int Scale { get; set; } = 1;
        public bool ShowRectangles { get; set; } = false;
    }
}