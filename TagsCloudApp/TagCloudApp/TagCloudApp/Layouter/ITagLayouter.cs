using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Geometry;

namespace TagCloudApp.Layouter
{
    public interface ITagLayouter
    {
        Rectangle PutNextTag(string tag, int frequence);
    }

    public static class TagLayouterHelper
    {
        public static IReadOnlyDictionary<string, Rectangle> PutManyTags(this ITagLayouter layouter, IReadOnlyDictionary<string, int> tags)
        {
            return
                tags.Select(p => new KeyValuePair<string, Rectangle>(p.Key, layouter.PutNextTag(p.Key, p.Value)))
                    .ToDictionary();
        }
    }
}