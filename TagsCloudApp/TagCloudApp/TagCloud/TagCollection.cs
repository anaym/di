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
        private readonly Dictionary<Result<string>, Result<int>> tags;

        public TagCollection(ITagExtractor extractor, ITagFilter[] filters)
        {
            this.extractor = extractor;
            this.filters = filters;
            tags = new Dictionary<Result<string>, Result<int>>();
        }

        public void Clear() => tags.Clear();

        public void AddWord(string word, int count = 1)
        {
            var tag = extractor.ExtractTag(Result<string>.Success(word));
            if (tag.IsFail) return;
            if (tags.ContainsKey(tag))
            {
                tags[tag] = tags[tag].Select(r => r + count);
            }
            else if (filters.All(f => f.IsCollectableTag(tag).Validate().IsSuccess))
            {
                tags.Add(tag, Result.Success(count));
            }
        }

        public void AddAnyWords(IEnumerable<Result<string>> words)
        {
            foreach (var word in words.GetSuccesful())
            {
                AddWord(word);
            }
        }

        public Result<int> MinFrequence => tags.Count == 0 ? Result.Success(0) : Result.Of(() => tags.GetSuccesful().Min(t => t.Value));
        public Result<int> MaxFrequence => tags.Count == 0 ? Result.Success(0) : Result.Of(() => tags.GetSuccesful().Max(t => t.Value));

        public IReadOnlyDictionary<Result<string>, Result<int>> GetTags() => tags.Where(p => p.IsSuccess()).OrderByDescending(p => p.GetValueOrThrow().Value).ToDictionary();
    }

}