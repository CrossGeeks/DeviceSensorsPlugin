using Android.Hardware;
using Android.Runtime;
using Java.Util.Concurrent;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.Android
{
    public abstract class BaseDeviceSensor<T> : Java.Lang.Object,IDeviceSensor<T>, ISensorEventListener
    {
        readonly SensorManager _sensorManager;
        readonly SensorType _sensorType;
        readonly Sensor _sensor;
        bool _isActive = false;
        T _lastReading;
        int _readingInterval;
        DateTime _lastReadingTime;
        bool firstRead = false;


        public BaseDeviceSensor(SensorManager sensorManager,SensorType sensorType)
        {
            _sensorManager = sensorManager;
            _sensorType = sensorType;
            _sensor = _sensorManager.GetDefaultSensor(_sensorType);
        }
        public bool IsSupported => _sensorManager.GetSensorList(_sensorType).Any();

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public T LastReading { get { return _lastReading; } }

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

        public event EventHandler<DeviceSensorReadingEventArgs<T>> OnReadingChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
          
        }

        protected abstract T GetReading(SensorEvent e);

        public void OnSensorChanged(SensorEvent e)
        {
            var currentReadingTime=DateTime.Now;

            if(firstRead  || (currentReadingTime -_lastReadingTime).TotalMilliseconds >= _readingInterval)
            {
                _lastReading = GetReading(e);
                OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<T>(_lastReading));
                _lastReadingTime = DateTime.Now;
                firstRead = false;
            }

        }

        public void StartReading(int readingInterval = -1)
        {
            firstRead = true;
            _sensorManager.RegisterListener(this, _sensor, SensorDelay.Fastest);

            if (readingInterval >= 0)
            {
                _readingInterval = readingInterval;
            }

            _isActive = true;
        }

        public void StopReading()
        {
            firstRead = false;
            _isActive = false;
            _sensorManager.UnregisterListener(this, _sensor);
        }


        /// <summary>
        /// Dispose of class and parent classes
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose up
        /// </summary>
        ~BaseDeviceSensor()
        {
            Dispose(false);
        }
        private bool disposed = false;
        /// <summary>
        /// Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    StopReading();
                    _sensor?.Dispose();
                   
                }

                disposed = true;
            }
        }
    }
}
