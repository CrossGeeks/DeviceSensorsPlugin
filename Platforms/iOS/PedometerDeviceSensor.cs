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

        static readonly Lazy<CMStepCounter> stepCounter = new Lazy<CMStepCounter>(() => new CMStepCounter());
        public bool IsSupported => CMStepCounter.IsStepCountingAvailable;

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

        public void StartReading(int reportInterval = -1)
        {
            firstRead = true;
            if (reportInterval >= 0)
            {
                _readingInterval = reportInterval;
            }
            stepCounter.Value.StartStepCountingUpdates(NSOperationQueue.CurrentQueue,1, OnPedometerChanged);
        }

        private void OnPedometerChanged(nint numberOfSteps, NSDate timestamp, NSError error)
        {
            var currentReadingTime = DateTime.Now;

            if (firstRead || (currentReadingTime - _lastReadingTime).TotalMilliseconds >= ReadingInterval)
            {
                lastReading = (int)numberOfSteps;
                OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<int>(lastReading));
                firstRead = false;
            }
        }

        public void StopReading()
        {
            firstRead = false;
            stepCounter.Value.StopStepCountingUpdates();
        }
        
    }
}
