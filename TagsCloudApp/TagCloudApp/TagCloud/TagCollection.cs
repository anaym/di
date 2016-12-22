using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Source;
using Utility;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud
{
    public class TagCollection
    {
        private readonly ITagExtractor extractor;
        private readonly ITagFilter[] filters;
        private readonly Dictionary<string, int> tags;

        public TagCollection(ITagExtractor extractor, ITagFilter[] filters)
        {
            this.extractor = extractor;
            this.filters = filters;
            tags = new Dictionary<string, int>();
        }

        public int MinFrequence => tags.Count == 0 ? 0 : tags.Min(t => t.Value);
        public int MaxFrequence => tags.Count == 0 ? 0 : tags.Max(t => t.Value);

        public IReadOnlyDictionary<string, int> GetTags() => tags.OrderByDescending(p => p.Value).ToDictionary();

        public void Clear() => tags.Clear();

        public void AddWord(string word, int count = 1)
        {
            extractor.ExtractTag(word).Select(t => AddTag(t, count));   
        }

        public void AddAnyWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                AddWord(word);
            }
        }

        private void AddTag(string tag, int count)
        {
            if (tags.ContainsKey(tag))
            {
                tags[tag] += count;
            }
            else if (filters.All(f => f.IsCollectableTag(tag).Validate().IsSuccess))
            {
                tags.Add(tag, count);
            }
        }
    }

}