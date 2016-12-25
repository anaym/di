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
            return sources
                .Select(s => s.GetWords())
                .Select(collection.AddAnyWords)
                .Select(r => r.RefineException("Load error"))
                .ToList();
        }

        public Result<Bitmap> Render()
        {
            return layouterFactory()
                .PutManyTags(collection.GetTags())
                .Select(r => rendererFactory().Render(r)).Unpack();
        }
    }
}