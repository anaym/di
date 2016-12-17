using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagCloudApp.Actions.File
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
            var path = app.RequestSavePath("out.png", ".png");
            if (path != null)
            {
                pictureBox.Image?.Save(path, ImageFormat.Png);
            }
        }
    }
}