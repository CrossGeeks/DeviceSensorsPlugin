using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Shared
{
    /// <summary>
	/// Device sensor reading interval enum.
	/// </summary>
	public enum DeviceSensorReadingInterval
    {
        /// <summary>
        /// Fastest Delay
        /// </summary>
		Fastest = 0,
        /// <summary>
        /// Game Delay
        /// </summary>
		Game = 20,
        /// <summary>
        /// Ui Delay
        /// </summary>
		Ui = 60,
        /// <summary>
        /// Default Delay
        /// </summary>
		Default = 200

    }

    public class DeviceSensorReadingEventArgs<T> : EventArgs
    {
        public T Reading { get; }

        public DeviceSensorReadingEventArgs(T reading)
        {
            Reading = reading;
        }

    }
    public interface IDeviceSensor<T>
    {
        bool IsSupported { get; }
        bool IsActive { get; }
        T LastReading {get;}
        int ReadingInterval { get; set; }
        void StartReading(int readingInterval = -1);
        void StopReading();

        event EventHandler<DeviceSensorReadingEventArgs<T>> OnReadingChanged;
    }
}
