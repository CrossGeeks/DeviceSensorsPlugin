using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Sensor;

namespace Plugin.DeviceSensors.Platforms.Tizen
{
    public class PedometerDeviceSensor : IDeviceSensor<int>
    {
        readonly Pedometer _pedometer;

        public PedometerDeviceSensor()
        {
            try
            {
                _pedometer = new Pedometer();
            }
            catch (NotSupportedException)
            {

            }
        }

        public bool IsSupported => Pedometer.IsSupported;

        public bool IsActive
        {
            get
            {
                return _pedometer?.IsSensing ?? false;
            }
        }

        public int LastReading
        {
            get
            {
                return (int)_pedometer.StepCount;
            }
        }

        public int ReadingInterval { get { return (int)_pedometer.Interval; } set { _pedometer.Interval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<int>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
               _pedometer.Interval = (uint)reportInterval;
            }

            _pedometer.DataUpdated += PedometerReadingChanged;
            _pedometer.Start();
        }

        private void PedometerReadingChanged(object sender, PedometerDataUpdatedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<int>((int)args.StepCount));
        }

        public void StopReading()
        {
            _pedometer.DataUpdated -= PedometerReadingChanged;
            _pedometer.Stop();
        }
    }
}
