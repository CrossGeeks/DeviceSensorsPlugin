using CoreMotion;
using Foundation;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.iOS
{
    public class GyroscopeDeviceSensor : IDeviceSensor<VectorReading>
    {
        CMMotionManager _motionManager;
        VectorReading _lastReading;

        public GyroscopeDeviceSensor(CMMotionManager motionManager)
        {
            _motionManager = motionManager;
        }

        public bool IsSupported => _motionManager.GyroAvailable;

        public bool IsActive { get { return _motionManager.GyroAvailable; } }

        public VectorReading LastReading
        {
            get
            {
                VectorReading lastReading = null;
                var data = _motionManager.GyroData;
                if (data != null)
                {
                    lastReading = new VectorReading(data.RotationRate.x, data.RotationRate.y, data.RotationRate.z);
                }

                return lastReading;
            }
        }

        public int ReadingInterval { get { return Convert.ToInt32(_motionManager.GyroUpdateInterval); } set { _motionManager.GyroUpdateInterval = value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;
        public event EventHandler<DeviceSensorReadingErrorEventArgs> OnReadingError;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval > 0)
            {
                _motionManager.GyroUpdateInterval = reportInterval;
            }

            try
            {
                _motionManager.StartGyroUpdates(NSOperationQueue.MainQueue, OnGyroscopeChanged);
            }
            catch (Exception ex)
            {
                OnReadingError?.Invoke(this, new DeviceSensorReadingErrorEventArgs("Gyroscope - Not called from the UIThread"));
            }
            
        }

        public void StopReading()
        {
            _motionManager.StopGyroUpdates();
        }


        /// <summary>
        /// Raises the gyroscope changed event.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="error">Error.</param>
        void OnGyroscopeChanged(CMGyroData data, NSError error)
        {
            if (error == null)
            {
                OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(data.RotationRate.x, data.RotationRate.y, data.RotationRate.z)));
            }
            else
            {
                OnReadingError?.Invoke(this, new DeviceSensorReadingErrorEventArgs(error.Description));
            }
        }
    }
}
