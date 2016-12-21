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
        public static Result<IReadOnlyDictionary<string, Rectangle>> PutManyTags(this ITagLayouter layouter, Result<IReadOnlyDictionary<string, int>> tags)
        {
            return tags.Select(t => t
                .Select(p => new KeyValuePair<string, Result<Rectangle>>(p.Key, layouter.PutNextTag(p.Key, p.Value)));
        }
    }
}