using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagCloudApp.App.GUI.Actions
{
    public class SaveImageUiAction : IUiAction
    {
        private readonly PictureBox pictureBox;

        public SaveImageUiAction(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        public string Category => "File";
        public string Name => "Save";
        public string Description => "Save image to file";
        public double Index => 0;
        public void Perform(IApplication app)
        {
            pictureBox.Image?.Save("s.png", ImageFormat.Png);
        }
    }
}