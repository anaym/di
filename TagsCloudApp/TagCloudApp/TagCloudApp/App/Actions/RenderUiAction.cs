using System;
using System.Windows.Forms;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;

namespace TagCloudApp.App.Actions
{
    public class RenderUiAction : IUiAction
    {
        private readonly Func<ITagLayouter> layouterFactory;
        private readonly TagCollection collection;
        private readonly ITagCloudRenderer renderer;
        private readonly PictureBox pictureBox;

        public RenderUiAction(Func<ITagLayouter> layouterFactory, TagCollection collection, ITagCloudRenderer renderer, PictureBox pictureBox)
        {
            this.layouterFactory = layouterFactory;
            this.collection = collection;
            this.renderer = renderer;
            this.pictureBox = pictureBox;
        }

        public string Category => "Render";
        public string Name => "Render";
        public string Description => "Render tag cloud";
        public double Index => 1.5;
        public void Perform(IApplication app)
        {
            var rectangles = layouterFactory().PutManyTags(collection.GetTags());
            var bitmap = renderer.Render(rectangles);

            pictureBox.Image = bitmap;
            if (bitmap != null)
            {
                pictureBox.Size = bitmap.Size;
            }
            pictureBox.Refresh();
            app.HasUnapplayedChanges = false;
        }
    }
}