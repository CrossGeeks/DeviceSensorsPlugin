using CoreLocation;
using CoreMotion;
using Plugin.DeviceSensors.Platforms.iOS;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors
{
    /// <summary>
    /// Interface for $safeprojectgroupname$
    /// </summary>
    public class DeviceSensorsManager : IDeviceSensors
    {
        static readonly Lazy<CMMotionManager> motionManager = new Lazy<CMMotionManager>(() => new CMMotionManager());
        static readonly Lazy<CLLocationManager> locationManager = new Lazy<CLLocationManager>(() => new CLLocationManager()
        {
            DesiredAccuracy = CLLocation.AccuracyBest,
            HeadingFilter = 1
        });

        static readonly Lazy<IDeviceSensor<VectorReading>> _accelerometer = new Lazy<IDeviceSensor<VectorReading>>(() => new AccelerometerDeviceSensor(motionManager.Value));
        static readonly Lazy<IDeviceSensor<VectorReading>> _gyroscope = new Lazy<IDeviceSensor<VectorReading>>(() => new GyroscopeDeviceSensor(motionManager.Value));
        static readonly Lazy<IDeviceSensor<VectorReading>> _magnetometer = new Lazy<IDeviceSensor<VectorReading>>(() => new MagnetometerDeviceSensor(motionManager.Value));
        static readonly Lazy<IDeviceSensor<CompassReading>> _compass = new Lazy<IDeviceSensor<CompassReading>>(() => new CompassDeviceSensor(locationManager.Value));
        static readonly Lazy<IDeviceSensor<double>> _barometer = new Lazy<IDeviceSensor<double>>(() => new BarometerDeviceSensor());
        static readonly Lazy<IDeviceSensor<int>> _pedometer = new Lazy<IDeviceSensor<int>>(() => new PedometerDeviceSensor());

        public IDeviceSensor<VectorReading> Accelerometer => _accelerometer.Value;

        public IDeviceSensor<VectorReading> Gyroscope => _gyroscope.Value;

        public IDeviceSensor<VectorReading> Magnetometer => _magnetometer.Value;

        public IDeviceSensor<CompassReading> Compass => _compass.Value;

        public IDeviceSensor<double> Barometer => _barometer.Value;

        public IDeviceSensor<int> Pedometer => _pedometer.Value;


    }
}
