﻿using TagCloud.Core.Layouter;

namespace TagCloudApp.Layouter
{
    public class EqualHeightExtractor : IHeightExtractor
    {
        public int ExtractHeight(int frequience)
        {
            return frequience;
        }
    }
}