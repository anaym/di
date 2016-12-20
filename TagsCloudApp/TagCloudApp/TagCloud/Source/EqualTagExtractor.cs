﻿using TagCloud.Core.Source;
using Utility.RailwayExceptions;

namespace TagCloud.Source
{
    public class EqualTagExtractor : ITagExtractor
    {
        public Result<string> ExtractTag(Result<string> word)
        {
            return word;
        }
    }
}