## DeviceSensors Plugin for Xamarin iOS, Android, UWP and Tizen
Simple cross platform plugin to access device sensors.

## Features

- Access to accelerometer
- Access to gyroscope
- Access to magnetometer
- Access to barometer
- Access to pedometer

### Setup
* Available on NuGet: http://www.nuget.org/packages/Plugin.DeviceSensors [![NuGet](https://img.shields.io/nuget/v/Plugin.DeviceSensors.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.DeviceSensors/)
* Install into your PCL project and Client projects.


**Platform Support**

|Platform|Version|
| ------------------- | :------------------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 13+|
|Windows 10 UWP|10+|
|Tizen|4.0+|

### API Usage

Call **CrossDeviceSensors.Current** from any project or PCL to gain access to APIs.

#### iOS Setup

Add to your Info.plist **NSMotionUsageDescription** key:

```xml
<key>NSMotionUsageDescription</key>
<string>This app needs to be able to access your motion use</string>
```

#### Getting Started

Device Sensor interface

```cs
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
```

Available sensors

```cs
    public interface IDeviceSensors
    {
       IDeviceSensor<VectorReading> Accelerometer {get; }
       IDeviceSensor<VectorReading> Gyroscope { get; }
       IDeviceSensor<VectorReading> Magnetometer { get; }
       IDeviceSensor<double> Barometer { get; }
       IDeviceSensor<int> Pedometer { get; }
    }
```
Usage sample:

Accelerometer
```csharp
    if(CrossDeviceSensors.Current.Accelerometer.IsSupported)
    {
         CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Accelerometer.StartReading();
    }
   

```


Gyroscope
```csharp
    if(CrossDeviceSensors.Current.Gyroscope.IsSupported)
    {
         CrossDeviceSensors.Current.Gyroscope.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Gyroscope.StartReading();
    }
   

```

Magnetometer
```csharp
    if(CrossDeviceSensors.Current.Magnetometer.IsSupported)
    {
         CrossDeviceSensors.Current.Magnetometer.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Magnetometer.StartReading();
    }
   

```


#### Contributors

* [Rendy Del Rosario](https://github.com/rdelrosario)
* [Jong Heon Choi](https://github.com/JongHeonChoi)
* [Dimitri Jevic](https://github.com/dimitrijevic)
