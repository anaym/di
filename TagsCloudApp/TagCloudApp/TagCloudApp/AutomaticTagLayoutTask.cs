using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudApp.IO;
using TagCloudApp.Layouter;
using TagCloudApp.TagCloudRender;
using TagCloudApp.WordToTag;
using Utility;

namespace TagCloudApp
{
    public class AutomaticTagLayoutTask : ITagLayoutTask
    {
        private readonly ITagLayouter layouter;
        private readonly ITagCloudRenderer renderer;
        private readonly IWordsSource source;
        private readonly ITagExtractor extractor;
        private readonly IReadOnlyList<ITagFilter> filters;

        public AutomaticTagLayoutTask(ITagLayouter layouter, ITagCloudRenderer renderer, IWordsSource source, ITagExtractor extractor, IEnumerable<ITagFilter> filters)
        {
            this.layouter = layouter;
            this.renderer = renderer;
            this.source = source;
            this.extractor = extractor;
            this.filters = filters.ToList();
        }

        public Bitmap Solve()
        {
            var words = source.GetWords();
            var tags = words.Select(extractor.ExtractTag).Where(t => filters.All(f => f.IsCollectedTag(t))).ToList();
            var frequences = tags.Distinct().Select(t => new KeyValuePair<string, int>(t, tags.Count(r => r == t))).ToDictionary();
            var rectangles = layouter.PutManyTags(frequences);
            return renderer.Render(rectangles);
        }
    }
}