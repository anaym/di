using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Task;

namespace TagCloudApp.Task
{
    public class TagLayoutTask : ITagLayoutTask
    {
        private readonly IReadOnlyDictionary<string, int> tags;
        private readonly ITagLayouter layouter;
        private readonly ITagCloudRenderer renderer;

        public TagLayoutTask(TagCollection tags, ITagLayouter layouter, ITagCloudRenderer renderer) : this(tags.GetTags(), layouter, renderer)
        { }

        public TagLayoutTask(IReadOnlyDictionary<string, int> tags, ITagLayouter layouter, ITagCloudRenderer renderer)
        {
            this.tags = tags;
            this.layouter = layouter;
            this.renderer = renderer;
        }

        public Bitmap Solve()
        {
            var rectangles = layouter.PutManyTags(tags);
            return renderer.Render(rectangles);
        }
    }
}