using TagCloud.Core.Layouter;
using Utility.Geometry;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Layouter
{
    public class TagLayouter : ITagLayouter
    {
        private readonly ISizeLayouter sizeLayouter;
        private readonly ISizeExtractor sizeExtractor;

        public TagLayouter(ISizeLayouter sizeLayouter, ISizeExtractor sizeExtractor)
        {
            this.sizeLayouter = sizeLayouter;
            this.sizeExtractor = sizeExtractor;
        }

        public Result<Rectangle> PutNextTag(Result<string> tag, Result<int> frequence)
        {
            return sizeLayouter.PutNextSize(sizeExtractor.ExtractSize(tag, frequence));
        }
    }
}