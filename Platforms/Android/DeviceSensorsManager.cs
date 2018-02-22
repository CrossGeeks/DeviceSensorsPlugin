using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Hardware;
using Plugin.DeviceSensors.Platforms.Android;
using Plugin.DeviceSensors.Shared;

namespace Plugin.DeviceSensors
{
    /// <summary>
    /// Interface for $safeprojectgroupname$
    /// </summary>
    public class DeviceSensorsManager : IDeviceSensors
    {
        static SensorManager _sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
        static readonly Lazy<IDeviceSensor<VectorReading>> _accelerometer = new Lazy<IDeviceSensor<VectorReading>>(() => new VectorDeviceSensor(_sensorManager,SensorType.Accelerometer));
        static readonly Lazy<IDeviceSensor<VectorReading>> _gyroscope = new Lazy<IDeviceSensor<VectorReading>>(() => new VectorDeviceSensor(_sensorManager, SensorType.Gyroscope));
        static readonly Lazy<IDeviceSensor<VectorReading>> _magnetometer = new Lazy<IDeviceSensor<VectorReading>>(() => new VectorDeviceSensor(_sensorManager, SensorType.MagneticField));
        static readonly Lazy<IDeviceSensor<int>> _pedometer = new Lazy<IDeviceSensor<int>>(() => new PedometerDeviceSensor(_sensorManager));
        static readonly Lazy<IDeviceSensor<double>> _barometer = new Lazy<IDeviceSensor<double>>(() => new BarometerDeviceSensor(_sensorManager));

        public IDeviceSensor<VectorReading> Accelerometer => _accelerometer.Value;

        public IDeviceSensor<VectorReading> Gyroscope => _gyroscope.Value;

        public IDeviceSensor<VectorReading> Magnetometer => _magnetometer.Value;

        //public IDeviceSensor<CompassReading> Compass => throw new NotImplementedException();

        public IDeviceSensor<double> Barometer => _barometer.Value;

        public IDeviceSensor<int> Pedometer => _pedometer.Value;
    }
}
