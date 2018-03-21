using System;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using MadworldStudios.Kookye.Drivers;
using System.Diagnostics;

namespace IotWaterSensor
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        private GpioPinValue value = GpioPinValue.High;
        private const int LED_PIN = 5;
        private GpioPin pin;
        private ThreadPoolTimer timer;

        private WaterSensor _waterSensor;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            InitGPIO();
            timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMilliseconds(500));            
        }
        private void InitGPIO()
        {
            _waterSensor = new WaterSensor();
        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            //read the water level
            var waterLevel = _waterSensor.GetWaterLevelPercentage();
            Debug.WriteLine($"Water Level: {waterLevel}%");
        }
    }
}
