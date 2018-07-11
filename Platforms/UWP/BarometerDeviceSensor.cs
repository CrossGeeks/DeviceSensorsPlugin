using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class BarometerDeviceSensor : IDeviceSensor<double>
    {
        readonly Barometer _barometer;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;


        public BarometerDeviceSensor()
        {
            _barometer = Barometer.GetDefault();
        }

        public bool IsSupported => _barometer != null;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public double LastReading
        {
            get
            {
                var reading = _barometer.GetCurrentReading();
                return reading.StationPressureInHectopascals;
            }
        }

        public int ReadingInterval { get { return (int)_barometer.ReportInterval; } set { _barometer.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<double>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _barometer.ReportInterval = (uint)reportInterval;
            }

            _barometer.ReadingChanged += BarometerReadingChanged;
        }

        private void BarometerReadingChanged(Barometer sender, BarometerReadingChangedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<double>(args.Reading.StationPressureInHectopascals));
        }

        public void StopReading()
        {
            _barometer.ReadingChanged -= BarometerReadingChanged;
        }
    }
}
