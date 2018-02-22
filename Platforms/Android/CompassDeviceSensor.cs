using System;
using System.Collections.Generic;
using System.Text;
using Android.Hardware;
using Plugin.DeviceSensors.Shared;

namespace Plugin.DeviceSensors.Platforms.Android
{
    public class CompassDeviceSensor : BaseDeviceSensor<CompassReading>
    {
        public CompassDeviceSensor(SensorManager sensorManager) : base(sensorManager, SensorType.All)
        {

        }

        protected override CompassReading GetReading(SensorEvent e)
        {
            // return new CompassReading(e.Values[0], e.Values[1], e.Values[2]);
            return null;
        }
    }
}
