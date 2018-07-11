using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class MagnetometerDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Magnetometer _magnetometer;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;


        public MagnetometerDeviceSensor()
        {
            _magnetometer = Magnetometer.GetDefault();
        }

        public bool IsSupported => _magnetometer != null;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public VectorReading LastReading
        {
            get
            {
                var reading = _magnetometer.GetCurrentReading();
                return new VectorReading(reading.MagneticFieldX, reading.MagneticFieldY, reading.MagneticFieldZ);
            }
        }

        public int ReadingInterval { get { return (int)_magnetometer.ReportInterval; } set { _magnetometer.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;
        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _magnetometer.ReportInterval = (uint)reportInterval;
            }
            _magnetometer.ReadingChanged += MagnetometerReadingChanged;
        }

        private void MagnetometerReadingChanged(Magnetometer sender, MagnetometerReadingChangedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.Reading.MagneticFieldX, args.Reading.MagneticFieldY, args.Reading.MagneticFieldZ)));
        }

        public void StopReading()
        {
            _magnetometer.ReadingChanged -= MagnetometerReadingChanged;
        }
    }
}
