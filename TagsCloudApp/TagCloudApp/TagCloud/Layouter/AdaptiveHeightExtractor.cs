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
            var delta = tagCollection.MaxFrequence.And(tagCollection.MinFrequence, (a, i) => a - i);
            var hdelta = settings.MaxCharHeight - settings.MinCharHeight;
            heightPerFrequence = delta.Select(d => 1.0*hdelta/d);
        }

        //TODO: стоит оборачивать в TryCatch??? (и во всех подобных местах)
        public Result<int> ExtractHeight(Result<int> frequence)
        {
            return heightPerFrequence
                .And(frequence, (h, f) => (int) (h*f))
                .Select(s => minCharHeight + s);
        }
    }
}