using CoreMotion;
using Foundation;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.iOS
{
    public class PedometerDeviceSensor : IDeviceSensor<int>
    {
        bool isActive = false;
        int lastReading = 0;
        DateTime _lastReadingTime;
        int _readingInterval;
        bool firstRead = false;

        static readonly Lazy<CMPedometer> stepCounter = new Lazy<CMPedometer>(() => new CMPedometer());
        public bool IsSupported => CMPedometer.IsStepCountingAvailable;

        public bool IsActive { get { return isActive; } }

        public int LastReading => lastReading;

        public int ReadingInterval
        {
            get
            {
                return _readingInterval;
            }
            set
            {
                _readingInterval = value;
                _lastReadingTime = DateTime.Now;
            }
        }

        public event EventHandler<DeviceSensorReadingEventArgs<int>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            firstRead = true;
            if (reportInterval >= 0)
            {
                _readingInterval = reportInterval;
            }
            stepCounter.Value.StartPedometerUpdates(NSDate.Now, OnPedometerChanged);
        }

        private void OnPedometerChanged(CMPedometerData pedometerData, NSError error)
        {
            if(error == null)
            {
                var currentReadingTime = DateTime.Now;

                if (firstRead || (currentReadingTime - _lastReadingTime).TotalMilliseconds >= ReadingInterval)
                {
                    lastReading = (int)pedometerData.NumberOfSteps;
                    OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<int>(lastReading));
                    firstRead = false;
                }
            }
            else
            {
                OnReadingError?.Invoke(this, new DeviceSensorReadingErrorEventArgs(error.Description));
            }
           
        }

        public void StopReading()
        {
            firstRead = false;
            stepCounter.Value.StopPedometerUpdates();
        }
        
    }
}
