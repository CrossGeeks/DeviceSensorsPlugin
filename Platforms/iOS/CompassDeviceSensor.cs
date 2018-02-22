using CoreLocation;
using Plugin.DeviceSensors.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Platforms.iOS
{
    public class CompassDeviceSensor : IDeviceSensor<CompassReading>
    {
        CLLocationManager _locationManager;
        bool isActive = false;
        public CompassDeviceSensor(CLLocationManager locationManager)
        {
            _locationManager = locationManager;
        }
        public bool IsSupported => CLLocationManager.HeadingAvailable;

        public bool IsActive { get { return isActive; } }

        public CompassReading LastReading { get { return new CompassReading(_locationManager.Heading.HeadingAccuracy < 0 ? CompassAccuracy.Unreliable : CompassAccuracy.High, _locationManager.Heading.MagneticHeading, _locationManager.Heading.TrueHeading); } }

        public int ReadingInterval { get; set; }

        public event EventHandler<DeviceSensorReadingEventArgs<CompassReading>> OnReadingChanged;

        public void StartReading(int interval = -1)
        {
            _locationManager.UpdatedHeading += UpdatedHeading;
            _locationManager.StartUpdatingHeading();
            isActive = true;
        }

        void UpdatedHeading(object sender, CLHeadingUpdatedEventArgs e)
        {
            OnReadingChanged?.Invoke(this, new DeviceSensorReadingEventArgs<CompassReading>(new CompassReading(e.NewHeading.HeadingAccuracy < 0 ? CompassAccuracy.Unreliable : CompassAccuracy.High,e.NewHeading.MagneticHeading,e.NewHeading.TrueHeading)));

        }

        public void StopReading()
        {
            isActive = false;
            _locationManager.UpdatedHeading -= UpdatedHeading;
            _locationManager.StopUpdatingHeading();
        }
    }
}
