using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Sensor;

namespace Plugin.DeviceSensors.Platforms.Tizen
{
    public class BarometerDeviceSensor : IDeviceSensor<double>
    {
        readonly PressureSensor _pressureSensor;

        public BarometerDeviceSensor()
        {
            try
            {
                _pressureSensor = new PressureSensor();
            }
            catch (NotSupportedException)
            {

            }
           
        }

        public bool IsSupported => PressureSensor.IsSupported;

        public bool IsActive
        {
            get
            {
                return _pressureSensor?.IsSensing ?? false;
            }
        }

        public double LastReading
        {
            get
            {
                return (double)_pressureSensor?.Pressure;
            }
        }

        public int ReadingInterval { get { return (int)_pressureSensor.Interval; } set { _pressureSensor.Interval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<double>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                _pressureSensor.Interval = (uint)reportInterval;
            }

            _pressureSensor.DataUpdated += PressureSensorReadingChanged;
            _pressureSensor.Start();
        }

        private void PressureSensorReadingChanged(object sender, PressureSensorDataUpdatedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<double>(args.Pressure));
        }

        public void StopReading()
        {
            _pressureSensor.DataUpdated -= PressureSensorReadingChanged;
            _pressureSensor.Stop();
        }
    }
}
