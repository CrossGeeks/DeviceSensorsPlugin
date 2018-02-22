using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Shared
{
    public enum CompassAccuracy
    {
        Unknown,
        Unreliable,
        Approximate,
        High
    }

    public class CompassReading
    {
        public CompassAccuracy Accuracy { get; }
        public double MagneticHeading { get; }
        public double? TrueHeading { get; }
        public CompassReading(CompassAccuracy accuracy, double magHeading, double? trueHeading)
        {
            Accuracy = accuracy;
            MagneticHeading = magHeading;
            TrueHeading = trueHeading;
        }
        
    }
}
