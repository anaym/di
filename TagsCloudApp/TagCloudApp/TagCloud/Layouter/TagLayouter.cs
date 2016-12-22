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

        public Result<Rectangle> PutNextTag(string tag, int frequence)
        {
            return sizeExtractor
                .ExtractSize(tag, frequence)
                .Select(sizeLayouter.PutNextSize).Unpack();
        }
    }
}