using System;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Extensions;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Source;
using TagCloud.Settings;
using Utility;

namespace TagCloud
{
    public class TagCloudCreator
    {
        private readonly Func<ITagLayouter> layouterFactory;
        private readonly CreatorSettings settings;
        private readonly Func<ITagCloudRenderer> rendererFactory;
        private readonly TagCollection collection;
        private readonly IFileWordsSource[] sources;

        public TagCloudCreator(TagCollection collection, IFileWordsSource[] sources, Func<ITagCloudRenderer> rendererFactory, Func<ITagLayouter> layouterFactory, CreatorSettings settings)
        {
            this.collection = collection;
            this.sources = sources;
            this.rendererFactory = rendererFactory;
            this.layouterFactory = layouterFactory;
            this.settings = settings;
        }

        public bool Load()
        {
            try
            {
                foreach (var source in sources)
                {
                    if (source.IsCanRead())
                    {
                        collection.Clear();
                        collection.AddAnyWords(source.GetWords().ToList());
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public Bitmap Render()
        {
            var rectangles = layouterFactory().PutManyTags(collection.GetTags().OrderByDescending(p => p.Value).Take(settings.TagCount).ToDictionary());
            return rendererFactory().Render(rectangles);
        }
    }
}