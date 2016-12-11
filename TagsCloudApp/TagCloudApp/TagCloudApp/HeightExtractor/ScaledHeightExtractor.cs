namespace TagCloudApp.HeightExtractor
{
    public class ScaledHeightExtractor : IHeightExtractor
    {
        private readonly int scale;

        public ScaledHeightExtractor(int scale = 16)
        {
            this.scale = scale;
        }

        public int ExtractHeight(int frequience)
        {
            return frequience*scale;
        }
    }
}