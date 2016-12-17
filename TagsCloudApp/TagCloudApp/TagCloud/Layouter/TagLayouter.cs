using TagCloud.Core.Layouter;
using Utility.Geometry;

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

        public Rectangle PutNextTag(string tag, int frequence)
        {
            var size = sizeExtractor.ExtractSize(tag, frequence);
            return sizeLayouter.PutNextSize(size);
        }
    }
}