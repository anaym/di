using System;
using System.Windows.Forms;
using TagCloud.Core.Task;

namespace TagCloudApp.App.Actions
{
    public class RenderUiAction : IUiAction
    {
        private readonly Func<ITagLayoutTask> taskFactory;
        private readonly PictureBox pictureBox;

        public RenderUiAction(Func<ITagLayoutTask> taskFactory, PictureBox pictureBox)
        {
            this.taskFactory = taskFactory;
            this.pictureBox = pictureBox;
        }

        public string Category => "Render";
        public string Name => "GO!";
        public string Description => "Render tag cloud";
        public double Index => 1.5;
        public void Perform(IApplication app)
        {
            var bitmap = taskFactory().Solve();
            pictureBox.Image = bitmap;
            if (bitmap != null)
            {
                pictureBox.Size = bitmap.Size;
            }
            pictureBox.Refresh();
        }
    }
}