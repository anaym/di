using System.Collections.Generic;
using System.Linq;
using Utility.Geometry;

namespace TagCloudApp.Layouter
{
    public interface ITagLayouter
    {
        Rectangle PutNextTag(string tag, int freauence);
    }

    public static class TagLayouterHelper
    {
        public static IEnumerable<Rectangle> PutManyTags(this ITagLayouter layouter, IReadOnlyDictionary<string, int> tags)
        {
            return tags.Select(p => layouter.PutNextTag(p.Key, p.Value));
        }
    }
}