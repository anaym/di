using System.Collections.Generic;
using TagCloud.Core.Layouter;
using Utility.Geometry;

namespace TagCloudApp.Layouter
{
    public class TagLayouter : ITagLayouter
    {
        private readonly ISizeLayouter sizeLayouter;
        private readonly ISizeExtractor sizeExtractor;
        private readonly Dictionary<string, Rectangle> tags;

        public TagLayouter(ISizeLayouter sizeLayouter, ISizeExtractor sizeExtractor)
        {
            this.sizeLayouter = sizeLayouter;
            this.sizeExtractor = sizeExtractor;
            tags = new Dictionary<string, Rectangle>();
        }

        public Rectangle PutNextTag(string tag, int frequence)
        {
            var size = sizeExtractor.ExtractSize(tag, frequence);
            return sizeLayouter.PutNextSize(size);
        }
    }
}