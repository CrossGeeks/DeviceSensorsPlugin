using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class PedometerDeviceSensor : IDeviceSensor<int>
    {
        Pedometer _pedometer;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;

        int _lastReading = -1;
        public PedometerDeviceSensor()
        {
           
        }

        //TODO: Check this condition
        public bool IsSupported => _pedometer != null;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public int LastReading
        {
            get
            {
                return _lastReading;
            }
        }

        public int ReadingInterval { get { return (int)_pedometer.ReportInterval; } set { _pedometer.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<int>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public async void StartReading(int reportInterval = -1)
        {
            _pedometer = await Pedometer.GetDefaultAsync();

            if (reportInterval >= 0)
            {
                _pedometer.ReportInterval = (uint)reportInterval;
            }
            
            _pedometer.ReadingChanged += PedometerReadingChanged;
        }

        private void PedometerReadingChanged(Pedometer sender, PedometerReadingChangedEventArgs args)
        {
            _lastReading = args.Reading.CumulativeSteps;
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<int>(_lastReading));
        }

        public void StopReading()
        {
            if(_pedometer!=null)
               _pedometer.ReadingChanged -= PedometerReadingChanged;
        }
    }
}
