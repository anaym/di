using TagCloud.Core.Layouter;

namespace TagCloudApp.Layouter
{
    public class AdaptiveHeightExtractor : IHeightExtractor
    {
        private readonly int minCharHeight;
        private readonly double heightPerFrequence;

        public AdaptiveHeightExtractor(int minTagFrequence, int maxTagFrequence, int minCharHeight, int maxCharHeight)
        {
            this.minCharHeight = minCharHeight;
            var delta = maxTagFrequence - minTagFrequence;
            var hdelta = maxCharHeight - minCharHeight;
            heightPerFrequence = 1.0*hdelta/delta;
        }
        public int ExtractHeight(int frequence)
        {
            return minCharHeight + (int)(heightPerFrequence*frequence);
        }
    }
}