using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class GyroscopeDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Gyrometer _gyrometer;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;


        public GyroscopeDeviceSensor()
        {
            _gyrometer = Gyrometer.GetDefault();
        }

        public bool IsSupported => _gyrometer != null;

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
                var reading = _gyrometer.GetCurrentReading();
                return new VectorReading(reading.AngularVelocityX, reading.AngularVelocityY, reading.AngularVelocityZ);
            }
        }

        public int ReadingInterval { get { return (int)_gyrometer.ReportInterval; } set { _gyrometer.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _gyrometer.ReportInterval = (uint)reportInterval;
            }
            _gyrometer.ReadingChanged += GyrometerReadingChanged;
        }

        private void GyrometerReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.Reading.AngularVelocityX, args.Reading.AngularVelocityY, args.Reading.AngularVelocityZ)));
        }

        public void StopReading()
        {
            _gyrometer.ReadingChanged -= GyrometerReadingChanged;
        }
    }
}
