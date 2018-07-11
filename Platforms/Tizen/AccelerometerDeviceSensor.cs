using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Sensor;

namespace Plugin.DeviceSensors.Platforms.Tizen
{
    public class AccelerometerDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Accelerometer _accelerometer;

        public AccelerometerDeviceSensor()
        {
            try
            {
                _accelerometer = new Accelerometer();
            }
            catch (NotSupportedException)
            {
                /// Accelerometer is not supported in the current device.
                /// You can also check whether the accelerometer is supported with the following property:
                /// var supported = Accelerometer.IsSupported;
            }
           
        }

        public bool IsSupported => Accelerometer.IsSupported;

        public bool IsActive
        {
            get
            {
                return _accelerometer?.IsSensing??false;
            }
        }

        public VectorReading LastReading
        {
            get
            {
                return new VectorReading(_accelerometer.X, _accelerometer.Y, _accelerometer.Z);
            }
        }

        public int ReadingInterval { get { return (int)_accelerometer.Interval; } set { _accelerometer.Interval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _accelerometer.Interval = (uint)reportInterval;
            }
            _accelerometer.DataUpdated += AccelerometerReadingChanged;
            _accelerometer.Start();
        }

        private void AccelerometerReadingChanged(object sender, AccelerometerDataUpdatedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.X, args.Y, args.Z)));
        }

        public void StopReading()
        {
            _accelerometer.DataUpdated -= AccelerometerReadingChanged;
            _accelerometer.Stop();
        }
    }
}
