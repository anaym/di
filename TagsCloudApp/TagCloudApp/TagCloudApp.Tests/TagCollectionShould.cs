using System;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Source;

namespace TagCloudApp.Tests
{
    [TestFixture]
    public class TagCollectionShould
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
            A.CallTo(() => fa.IsCollectedTag(A<string>.Ignored)).Returns(true);
            A.CallTo(() => fb.IsCollectedTag(A<string>.Ignored)).Returns(true);
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" };
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(words);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(words);
            A.CallTo(() => fa.IsCollectedTag(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => fb.IsCollectedTag(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).MustHaveHappened();
        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void NotContainsTag_WhenTagNonPassAtLeastOneFilter(bool passFirstFilter, bool passSecondFilter)
        {
            A.CallTo(() => fa.IsCollectedTag(A<string>.Ignored)).Returns(passFirstFilter);
            A.CallTo(() => fb.IsCollectedTag(A<string>.Ignored)).Returns(passSecondFilter);
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" };
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(words);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(Enumerable.Empty<string>());
        }

        [Test]
        public void ExtractTagsFromWord()
        {
            A.CallTo(() => fa.IsCollectedTag(A<string>.Ignored)).Returns(true);
            A.CallTo(() => fb.IsCollectedTag(A<string>.Ignored)).Returns(true);
            //TODO: как сделать красивее???
            var words = new[] { "a", "bc", "def" };
            var processedWords = new[] {"_a", "_bc_", "def_"};
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).ReturnsNextFromSequence(processedWords);

            collection.AddAnyWords(words);

            collection.GetTags().Keys.ShouldBeEquivalentTo(processedWords);
            A.CallTo(() => extractor.ExtractTag(A<string>.Ignored)).MustHaveHappened();
        }
    }
}