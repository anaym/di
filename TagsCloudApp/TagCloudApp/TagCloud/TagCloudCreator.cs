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

        // CR: Loading words only from the first
        // successfull source is EXTREMELY counterintuitive
        public List<Result<None>> Load()
        {
            // CR: Compare to
            // return sources.GetWords().Select(words => collection.AddAnyWords(words));
            var results = new List<Result<None>>();
            foreach (var source in sources)
            {
                var words = source.GetWords();
                results.Add(words.IgnoreValue().RefineError("Load error"));
                if (words.IsSuccess)
                {
                    collection.Clear();
                    // CR: You're returning Result<T> AND throwing
                    // Why bother using Result<T> then?
                    collection.AddAnyWords(source.GetWords().GetValueOrThrow());
                    break;
                }
            }
            return results;
        }

        public Result<Bitmap> Render()
        {
            return layouterFactory()
                .PutManyTags(collection.GetTags())
                .Select(r => rendererFactory().Render(r)).Unpack();
        }
    }
}