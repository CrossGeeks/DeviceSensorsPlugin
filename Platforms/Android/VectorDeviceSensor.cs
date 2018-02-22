using Android.Hardware;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.Android
{
    public class VectorDeviceSensor : BaseDeviceSensor<VectorReading>
    {
        public VectorDeviceSensor(SensorManager sensorManager, SensorType sensorType) : base (sensorManager,sensorType)
        {

        }
        protected override VectorReading GetReading(SensorEvent e)
        {
            return new VectorReading(e.Values[0], e.Values[1], e.Values[2]);
        }
    }
}
