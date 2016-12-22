﻿using Utility.RailwayExceptions;

namespace TagCloud.Core.Source
{
    public interface ITagExtractor
    {
        Result<string> ExtractTag(string word);
    }
}