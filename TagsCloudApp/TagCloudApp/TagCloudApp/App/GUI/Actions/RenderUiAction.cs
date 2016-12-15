using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagCloudApp.TagCloudRender;
using Rectangle = Utility.Geometry.Rectangle;

namespace TagCloudApp.App.GUI.Actions
{
    public class RenderUiAction : IUiAction
    {
        private readonly Dictionary<string, Rectangle> data;
        private readonly ITagCloudRenderer renderer;
        private readonly PictureBox pictureBox;

        public RenderUiAction(Dictionary<string, Rectangle> data, ITagCloudRenderer renderer, PictureBox pictureBox)
        {
            this.data = data;
            this.renderer = renderer;
            this.pictureBox = pictureBox;
        }

        public string Category => "Render";
        public string Name => "GO!";
        public string Description => "Render tag cloud";
        public double Index => 1.5;
        public void Perform(IApplication app)
        {
            var bitmap = renderer.Render(data);
            pictureBox.Image = bitmap;
            if (bitmap != null)
            {
                pictureBox.Size = bitmap.Size;
            }
            pictureBox.Refresh();
        }
    }
}