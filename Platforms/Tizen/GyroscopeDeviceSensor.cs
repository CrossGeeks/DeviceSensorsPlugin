using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Sensor;

namespace Plugin.DeviceSensors.Platforms.Tizen
{
    public class GyroscopeDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Gyroscope _gyroscope;

        public GyroscopeDeviceSensor()
        {
            try
            {
                _gyroscope = new Gyroscope();
            }
            catch (NotSupportedException)
            {

            }
          
        }

        public bool IsSupported => Gyroscope.IsSupported;

        public bool IsActive
        {
            get
            {
                return _gyroscope?.IsSensing ?? false;
            }
        }

        public VectorReading LastReading
        {
            get
            {
                return new VectorReading(_gyroscope.X, _gyroscope.Y, _gyroscope.Z);
            }
        }

        public int ReadingInterval { get { return (int)_gyroscope.Interval; } set { _gyroscope.Interval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _gyroscope.Interval = (uint)reportInterval;
            }

            _gyroscope.DataUpdated += GyrosocopeReadingChanged;
            _gyroscope.Start();
        }

        private void GyrosocopeReadingChanged(object sender, GyroscopeDataUpdatedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.X, args.Y, args.Z)));
        }

        public void StopReading()
        {
            _gyroscope.DataUpdated -= GyrosocopeReadingChanged;
            _gyroscope.Stop();
        }
    }
}
