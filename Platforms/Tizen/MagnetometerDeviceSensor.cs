using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Sensor;

namespace Plugin.DeviceSensors.Platforms.Tizen
{
    public class MagnetometerDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Magnetometer _magnetometer;

        public MagnetometerDeviceSensor()
        {
            try
            {
                _magnetometer = new Magnetometer();
            }
            catch (NotSupportedException)
            {

            }
        }

        public bool IsSupported => Magnetometer.IsSupported;

        public bool IsActive
        {
            get
            {
                return _magnetometer?.IsSensing ?? false;
            }
        }

        public VectorReading LastReading
        {
            get
            {
                return new VectorReading(_magnetometer.X, _magnetometer.Y, _magnetometer.Z);
            }
        }

        public int ReadingInterval { get { return (int)_magnetometer.Interval; } set { _magnetometer.Interval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _magnetometer.Interval = (uint)reportInterval;
            }

            _magnetometer.DataUpdated += MagnetometerReadingChanged;
            _magnetometer.Start();
        }

        private void MagnetometerReadingChanged(object sender, MagnetometerDataUpdatedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.X, args.Y, args.Z)));
        }

        public void StopReading()
        {
            _magnetometer.DataUpdated -= MagnetometerReadingChanged;
            _magnetometer.Stop();
        }
    }
}
