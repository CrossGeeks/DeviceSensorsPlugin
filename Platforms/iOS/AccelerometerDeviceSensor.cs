using CoreMotion;
using Foundation;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Plugin.DeviceSensors.Platforms.iOS
{
    public class AccelerometerDeviceSensor : IDeviceSensor<VectorReading>
    {
        CMMotionManager _motionManager;
        VectorReading _lastReading;

        public AccelerometerDeviceSensor(CMMotionManager motionManager)
        {
            _motionManager = motionManager;
        }

        public bool IsSupported => _motionManager.AccelerometerAvailable;

        public bool IsActive { get { return _motionManager.AccelerometerActive; } }

        public VectorReading LastReading
        {
            get
            {
                VectorReading lastReading = null;
                var data = _motionManager.AccelerometerData;
                if ( data !=null)
                {
                    lastReading=new VectorReading(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);
                }

                return lastReading;
            }
        }

        public int ReadingInterval { get { return Convert.ToInt32(_motionManager.AccelerometerUpdateInterval); } set { _motionManager.AccelerometerUpdateInterval = value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;

        public void StartReading(int reportInterval = -1)
        {
            if(reportInterval >= 0)
            {
                _motionManager.AccelerometerUpdateInterval = reportInterval;
            }
            
            _motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, OnAccelerometerChanged);
        }

        public void StopReading()
        {
            _motionManager.StopAccelerometerUpdates();
        }


        /// <summary>
        /// Raises the accelerometer changed event.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="error">Error.</param>
        void OnAccelerometerChanged(CMAccelerometerData data, NSError error)
        {
            OnReadingChanged?.Invoke(this,new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z)));
        }
    }
}
