using TagCloud.Core.Layouter;

namespace TagCloud.Layouter
{
    public class ScaledHeightExtractor : IHeightExtractor
    {
        private readonly int scale;

        public ScaledHeightExtractor(int scale = 16)
        {
            this.scale = scale;
        }

        public int ExtractHeight(int frequence)
        {
            return frequence*scale;
        }
    }
}