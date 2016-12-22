using TagCloud.Core.Layouter;
using TagCloud.Settings;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.Layouter
{
    public class AdaptiveHeightExtractor : IHeightExtractor
    {
        private readonly int minCharHeight;
        private readonly Result<double> heightPerFrequence;

        public AdaptiveHeightExtractor(TagCollection tagCollection, LayouterSettings settings)
        {
            minCharHeight = settings.MinCharHeight;
            var delta = tagCollection.MaxFrequence - tagCollection.MinFrequence;
            var hdelta = settings.MaxCharHeight - settings.MinCharHeight;
            heightPerFrequence = Result.Of(() => 1.0*hdelta/delta);
        }

        public Result<int> ExtractHeight(int frequence)
        {
            return heightPerFrequence
                .Select(h => (int) (h*frequence))
                .Select(s => minCharHeight + s);
        }
    }
}