using System;
using System.Collections.Generic;

namespace TagCloud.Settings
{
    public class CreatorSettings : ISettings
    {
        public int TagCount { get; set; } = 32;
        public string GetSettingsName() => "Creator";
    }
}