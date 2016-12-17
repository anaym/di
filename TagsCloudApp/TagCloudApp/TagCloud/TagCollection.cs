using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Source;
using Utility;

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

        public void Clear() => tags.Clear();

        public void AddWord(string word, int count = 1)
        {
            var tag = extractor.ExtractTag(word);
            if (tags.ContainsKey(tag))
            {
                tags[tag] += count;
            }
            else if (filters.All(f => f.IsCollectableTag(tag)))
            {
                tags.Add(tag, count);
            }
        }

        public void AddAnyWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                AddWord(word);
            }
        }

        public int MinFrequence => tags.Count == 0 ? 0 : tags.Min(t => t.Value);
        public int MaxFrequence => tags.Count == 0 ? 0 : tags.Max(t => t.Value);

        public IReadOnlyDictionary<string, int> GetTags() => tags.OrderByDescending(p => p.Value).ToDictionary();
    }

}