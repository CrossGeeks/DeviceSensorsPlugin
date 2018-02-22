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


Usage sample:

Accelerometer
```csharp
    if(CrossDeviceSensors.Current.Accelerometer.IsSupported)
    {
         CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Accelerometer.Start();
    }
   

```


Gyroscope
```csharp
    if(CrossDeviceSensors.Current.Gyroscope.IsSupported)
    {
         CrossDeviceSensors.Current.Gyroscope.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Gyroscope.Start();
    }
   

```

Magnetometer
```csharp
    if(CrossDeviceSensors.Current.Magnetometer.IsSupported)
    {
         CrossDeviceSensors.Current.Magnetometer.OnReadingChanged += (s,a)=>{

         };
         CrossDeviceSensors.Current.Magnetometer.Start();
    }
   

```


#### Contributors

* [Rendy Del Rosario](https://github.com/rdelrosario)
* [Jong Heon Choi](https://github.com/JongHeonChoi)
* [Dimitri Jevic](https://github.com/dimitrijevic)