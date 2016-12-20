﻿using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Source;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Tests
{
    [TestFixture]
    public class TagCollection_Should
    {
        private ITagFilter[] filters;
        private ITagExtractor extractor;
        private TagCollection collection;
        private ITagFilter fb;
        private ITagFilter fa;

        [SetUp]
        public void SetUp()
        {
            fa = A.Fake<ITagFilter>();
            fb = A.Fake<ITagFilter>();
            filters = new[] {fa, fb};
            extractor = A.Fake<ITagExtractor>();
            collection = new TagCollection(extractor, filters);
        }

        [Test]
        public void ContainsAllGoodTag_AfterAdd()
        {
            A.CallTo(() => fa.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(true));
            A.CallTo(() => fa.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(true));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.Select(Result.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<Result<string>>.Ignored)).ReturnsNextFromSequence(words);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(words);
            A.CallTo(() => fa.IsCollectableTag(A<Result<string>>.Ignored)).MustHaveHappened();
            A.CallTo(() => fb.IsCollectableTag(A<Result<string>>.Ignored)).MustHaveHappened();
            A.CallTo(() => extractor.ExtractTag(A<Result<string>>.Ignored)).MustHaveHappened();
        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void NotContainsTag_WhenTagNonPassAtLeastOneFilter(bool passFirstFilter, bool passSecondFilter)
        {
            A.CallTo(() => fa.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(passFirstFilter));
            A.CallTo(() => fb.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(passSecondFilter));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.Select(Result.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<Result<string>>.Ignored)).ReturnsNextFromSequence(words);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(Enumerable.Empty<Result<string>>());
        }

        [Test]
        public void ExtractTagsFromWord()
        {
            A.CallTo(() => fa.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(true));
            A.CallTo(() => fb.IsCollectableTag(A<Result<string>>.Ignored)).Returns(Result.Success(true));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.Select(Result.Success).ToArray();
            var processedWords = new[] {"_a", "_bc_", "def_"}.Select(Result.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<Result<string>>.Ignored)).ReturnsNextFromSequence(processedWords);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(processedWords);
            A.CallTo(() => extractor.ExtractTag(A<Result<string>>.Ignored)).MustHaveHappened();
        }
    }
}