using System;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Extensions;
using TagCloud.Core.Layouter;
using TagCloud.Core.Renderer;
using TagCloud.Core.Source;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud
{
    public class TagCloudCreator
    {
        private readonly Func<ITagLayouter> layouterFactory;
        private readonly Func<ITagCloudRenderer> rendererFactory;
        private readonly TagCollection collection;
        private readonly IFileWordsSource[] sources;

        public TagCloudCreator(TagCollection collection, IFileWordsSource[] sources, Func<ITagCloudRenderer> rendererFactory, Func<ITagLayouter> layouterFactory)
        {
            this.collection = collection;
            this.sources = sources;
            this.rendererFactory = rendererFactory;
            this.layouterFactory = layouterFactory;
        }

        public bool Load()
        {
            try
            {
                //TODO: упростить
                foreach (var source in sources.Where(s => s.IsCanRead().Validate().IsSuccess))
                { 
                    collection.Clear();
                    collection.AddAnyWords(source.GetWords().GetValueOrThrow().Select(Result.Success));
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public Result<Bitmap> Render()
        {
            var rectangles = layouterFactory().PutManyTags(collection.GetTags());
            return rendererFactory().Render(rectangles);
        }
    }
}