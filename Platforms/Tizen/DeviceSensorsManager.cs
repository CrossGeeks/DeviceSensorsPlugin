using System;
using System.Collections.Generic;
using System.Text;
using Plugin.DeviceSensors.Platforms.Tizen;
using Plugin.DeviceSensors.Shared;

namespace Plugin.DeviceSensors
{
    /// <summary>
    /// Interface for $safeprojectgroupname$
    /// </summary>
    public class DeviceSensorsManager : IDeviceSensors
    {
        static readonly Lazy<IDeviceSensor<VectorReading>> _accelerometer = new Lazy<IDeviceSensor<VectorReading>>(() => new AccelerometerDeviceSensor());
        static readonly Lazy<IDeviceSensor<VectorReading>> _gyroscope = new Lazy<IDeviceSensor<VectorReading>>(() => new GyroscopeDeviceSensor());
        static readonly Lazy<IDeviceSensor<VectorReading>> _magnetometer = new Lazy<IDeviceSensor<VectorReading>>(() => new MagnetometerDeviceSensor());
        //static readonly Lazy<IDeviceSensor<CompassReading>> _compass = new Lazy<IDeviceSensor<CompassReading>>(() => new CompassDeviceSensor());
        static readonly Lazy<IDeviceSensor<double>> _barometer = new Lazy<IDeviceSensor<double>>(() => new BarometerDeviceSensor());
        static readonly Lazy<IDeviceSensor<int>> _pedometer = new Lazy<IDeviceSensor<int>>(() => new PedometerDeviceSensor());

        public IDeviceSensor<VectorReading> Accelerometer => _accelerometer.Value;

        public IDeviceSensor<VectorReading> Gyroscope => _gyroscope.Value;

        public IDeviceSensor<VectorReading> Magnetometer => _magnetometer.Value;

        public IDeviceSensor<double> Barometer => _barometer.Value;

        public IDeviceSensor<int> Pedometer => _pedometer.Value;
    }
}
