using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace TagCloud.Settings
{
    public class RendererSettings : ISettings
    {
        public List<Color> TextColors { get; set; } = new List<Color> {Color.Red};
        public int Scale { get; set; } = 1;
        public bool ShowRectangles { get; set; } = false;
        public GenericFontFamilies Font { get; set; } = GenericFontFamilies.Monospace;
        public string SettingsName => "Renderer";
    }
}