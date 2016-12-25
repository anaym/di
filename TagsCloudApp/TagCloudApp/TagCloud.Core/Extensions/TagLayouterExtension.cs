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
        public static Result<IReadOnlyDictionary<string, Rectangle>> PutManyTags(this ITagLayouter layouter, IReadOnlyDictionary<string, int> tags)
        {
            return Results
                .Of(() => tags.ToDictionary(p => p.Key, p => layouter.PutNextTag(p.Key, p.Value).GetValueOrThrow()))
                .Cast<Dictionary<string, Rectangle>, IReadOnlyDictionary<string, Rectangle>>();
        }
    }
}