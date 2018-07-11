using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;
using CReading = Plugin.DeviceSensors.Shared.CompassReading;
namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class CompassDeviceSensor : IDeviceSensor<CReading>
    {
        readonly Compass _compass;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;


        public CompassDeviceSensor()
        {
            _compass = Compass.GetDefault();
        }

        public bool IsSupported => _compass != null;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public CReading LastReading
        {
            get
            {
                var reading = _compass.GetCurrentReading();
                return new CReading(TranslateAccuracy(reading.HeadingAccuracy),reading.HeadingMagneticNorth,reading.HeadingTrueNorth);
            }
        }

        public int ReadingInterval { get { return (int)_compass.ReportInterval; } set { _compass.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<CReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
               _compass.ReportInterval = (uint)reportInterval;
            }
            _compass.ReadingChanged += CompassReadingChanged;
        }

        private void CompassReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<CReading>(new CReading(TranslateAccuracy(args.Reading.HeadingAccuracy), args.Reading.HeadingMagneticNorth, args.Reading.HeadingTrueNorth)));
        }

        public void StopReading()
        {
            _compass.ReadingChanged -= CompassReadingChanged;
        }


        protected CompassAccuracy TranslateAccuracy(MagnetometerAccuracy accuracy)
        {
            var retVal = CompassAccuracy.Unknown;
            switch (accuracy)
            {
                case MagnetometerAccuracy.High:
                    retVal = CompassAccuracy.High;
                    break;
                case MagnetometerAccuracy.Unreliable:
                    retVal = CompassAccuracy.Unreliable;
                    break;
                case MagnetometerAccuracy.Approximate:
                    retVal = CompassAccuracy.Approximate;
                    break;
            }
            return retVal;
        }
    }
}
