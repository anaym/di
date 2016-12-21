using System;
using System.Collections.Generic;
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
        private readonly IWordsSource[] sources;

        public TagCloudCreator(TagCollection collection, IWordsSource[] sources, Func<ITagCloudRenderer> rendererFactory, Func<ITagLayouter> layouterFactory)
        {
            this.collection = collection;
            this.sources = sources;
            this.rendererFactory = rendererFactory;
            this.layouterFactory = layouterFactory;
        }

        public List<Result<None>> Load()
        {
            var results = new List<Result<None>>();
            foreach (var source in sources)
            {
                var words = source.GetWords();
                results.Add(words.IgnoreValue().RefineError("Load error"));
                if (words.IsSuccess)
                {
                    collection.Clear();
                    collection.AddAnyWords(source.GetWords().GetValueOrThrow().Select(Result.Success));
                    break;
                }
            }
            return results;
        }

        public Result<Bitmap> Render()
        {
            var rectangles = layouterFactory().PutManyTags(collection.GetTags());
            return rendererFactory().Render(rectangles);
        }
    }
}