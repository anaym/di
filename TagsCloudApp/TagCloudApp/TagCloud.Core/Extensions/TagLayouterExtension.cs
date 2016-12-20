using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Layouter;
using Utility.Geometry;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Core.Extensions
{
    public static class TagLayouterExtension
    {
        public static Result<Rectangle> PutNextTag(this ITagLayouter layouter, Result<KeyValuePair<string, int>> tagToFrequence)
        {
            return layouter.PutNextTag(tagToFrequence.Select(t => t.Key), tagToFrequence.Select(t => t.Value));
        }

        public static IReadOnlyDictionary<Result<string>, Result<Rectangle>> PutManyTags(this ITagLayouter layouter, IReadOnlyDictionary<Result<string>, Result<int>> tags)
        {
            return tags.ToDictionary(p => p.Key, p => layouter.PutNextTag(p.Key, p.Value));
        }
    }
}