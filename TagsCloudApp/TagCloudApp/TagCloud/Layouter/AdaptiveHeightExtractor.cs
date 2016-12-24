using TagCloud.Core.Layouter;
using TagCloud.Settings;

namespace TagCloud.Layouter
{
    public class AdaptiveHeightExtractor : IHeightExtractor
    {
        private readonly int minCharHeight;
        private readonly double heightPerFrequence;

        public AdaptiveHeightExtractor(TagCollection tagCollection, LayouterSettings settings)
        {
            minCharHeight = settings.MinCharHeight;
            var delta = tagCollection.MaxFrequence - tagCollection.MinFrequence;
            var hdelta = settings.MaxCharHeight - settings.MinCharHeight;
            heightPerFrequence = delta == 0 ? 1 : 1.0*hdelta/delta;
        }
        public int ExtractHeight(int frequence)
        {
            return minCharHeight + (int)(heightPerFrequence*frequence);
        }
    }
}