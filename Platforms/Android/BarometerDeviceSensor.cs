using Android.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.Android
{
    public class BarometerDeviceSensor : BaseDeviceSensor<double>
    {
        public BarometerDeviceSensor(SensorManager sensorManager) : base(sensorManager, SensorType.Pressure)
        {

        }
        protected override double GetReading(SensorEvent e)
        {
            return e.Values[0];
        }
    }
}
