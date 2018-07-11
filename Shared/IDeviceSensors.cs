using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors
{
    
    public interface IDeviceSensors
    {
       IDeviceSensor<VectorReading> Accelerometer {get; }
       IDeviceSensor<VectorReading> Gyroscope { get; }
       IDeviceSensor<VectorReading> Magnetometer { get; }
       IDeviceSensor<double> Barometer { get; }
       IDeviceSensor<int> Pedometer { get; }
    }
}
