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

    /// <summary>
    /// Device sensor reading event arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceSensorReadingEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Sensor reading
        /// </summary>
        public T Reading { get; }

        public DeviceSensorReadingEventArgs(T reading)
        {
            Reading = reading;
        }

    }
    public interface IDeviceSensor<T>
    {
        /// <summary>
        /// Check if sensor supported
        /// </summary>
        bool IsSupported { get; }
        /// <summary>
        /// Check if sensor is active
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// Get latest sensor reading
        /// </summary>
        T LastReading {get;}
        /// <summary>
        /// Sets/get sensor report interval
        /// </summary>
        int ReadingInterval { get; set; }
        /// <summary>
        /// Starts sensor reading
        /// </summary>
        void StartReading(int readingInterval = -1);
        /// <summary>
        /// Stops sensor reading
        /// </summary>
        void StopReading();

        /// <summary>
        /// Sensor reading changes event
        /// </summary>
        event EventHandler<DeviceSensorReadingEventArgs<T>> OnReadingChanged;
    }
}
