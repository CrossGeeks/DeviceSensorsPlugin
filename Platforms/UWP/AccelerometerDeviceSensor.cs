using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace Plugin.DeviceSensors.Platforms.UWP
{
    public class AccelerometerDeviceSensor : IDeviceSensor<VectorReading>
    {
        readonly Accelerometer accelerometer;
        DeviceSensorReadingInterval _readingInterval;
        bool _isActive = false;


        public AccelerometerDeviceSensor()
        {
            accelerometer = Accelerometer.GetDefault();
        }

        public bool IsSupported => accelerometer!=null;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public VectorReading LastReading {
            get
            {
                var reading= accelerometer.GetCurrentReading();
                return new VectorReading(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ);
            }
        }

        public int ReadingInterval { get { return (int)accelerometer.ReportInterval; } set { accelerometer.ReportInterval = (uint)value; } }

        public event EventHandler<DeviceSensorReadingEventArgs<VectorReading>> OnReadingChanged;

        public void StartReading(int reportInterval = -1)
        {
            if (reportInterval >= 0)
            {
                accelerometer.ReportInterval = (uint)reportInterval;
            }

            accelerometer.ReadingChanged += AccelerometerReadingChanged;
        }

        private void AccelerometerReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<VectorReading>(new VectorReading(args.Reading.AccelerationX, args.Reading.AccelerationY, args.Reading.AccelerationZ)));
        }

        public void StopReading()
        {
            accelerometer.ReadingChanged -= AccelerometerReadingChanged;
        }
    }
}
