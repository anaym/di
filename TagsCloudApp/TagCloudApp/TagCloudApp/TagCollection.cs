using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TagCloudApp.WordToTag;
using Utility;

namespace TagCloudApp
{
    public class TagCollection
    {
        private readonly ITagExtractor extractor;
        private readonly ITagFilter[] filters;
        private Dictionary<string, int> tags;

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
            else if (filters.All(f => f.IsCollectedTag(tag)))
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

        public IReadOnlyDictionary<string, int> GetTags() => tags.ToDictionary();
    }

}