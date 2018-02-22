using System;
using System.Collections.Generic;
using System.Text;
using Android.Hardware;

namespace Plugin.DeviceSensors.Platforms.Android
{
    public class PedometerDeviceSensor : BaseDeviceSensor<int>
    {
        public PedometerDeviceSensor(SensorManager sensorManager) : base(sensorManager, SensorType.StepCounter)
        {

        }
        protected override int GetReading(SensorEvent e)
        {
            return Convert.ToInt32(e.Values[0]);
        }
    }
}
