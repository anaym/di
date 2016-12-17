using TagCloud.Core.Layouter;

namespace TagCloud.Layouter
{
    public class EqualHeightExtractor : IHeightExtractor
    {
        public int ExtractHeight(int frequence)
        {
            return frequence;
        }
    }
}