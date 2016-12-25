using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Source;
using Utility.RailwayExceptions;

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
            filters = new[] { fa, fb };
            extractor = A.Fake<ITagExtractor>();
            collection = new TagCollection(extractor, filters);
        }

        [Test]
        public void ContainsAllGoodTag_AfterAdd()
        {
            A.CallTo(() => fa.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(true));
            A.CallTo(() => fb.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(true));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.ToArray();
            var processedWords = words.Select(Results.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(processedWords);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(words);
            A.CallTo(() => fa.IsCollectableTag(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => fb.IsCollectableTag(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).MustHaveHappened();
        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void NotContainsTag_WhenTagNonPassAtLeastOneFilter(bool passFirstFilter, bool passSecondFilter)
        {
            A.CallTo(() => fa.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(passFirstFilter));
            A.CallTo(() => fb.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(passSecondFilter));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.ToArray();
            var processedWords = words.Select(Results.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(processedWords);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(Enumerable.Empty<string>());
        }

        [Test]
        public void ExtractTagsFromWord()
        {
            A.CallTo(() => fa.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(true));
            A.CallTo(() => fb.IsCollectableTag(A<string>.Ignored)).Returns(Results.Success(true));
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" }.ToArray();
            var processedWords = new[] { "_a", "_bc_", "def_" }.Select(Results.Success).ToArray();
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(processedWords);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(processedWords.Select(v => v.GetValueOrThrow()));
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).MustHaveHappened();
        }
    }
}