using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Layouter;
using Utility.Geometry;

namespace TagCloud.Core.Extensions
{
    public static class TagLayouterExtension
    {
        public static Rectangle PutNextTag(this ITagLayouter layouter, KeyValuePair<string, int> tagToFrequence)
        {
            return layouter.PutNextTag(tagToFrequence.Key, tagToFrequence.Value);
        }

        public static IReadOnlyDictionary<string, Rectangle> PutManyTags(this ITagLayouter layouter, IReadOnlyDictionary<string, int> tags)
        {
            return tags.ToDictionary(p => p.Key, layouter.PutNextTag);
        }
    }
}