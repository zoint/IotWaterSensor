using System;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using MadworldStudios.Kookye.Drivers;

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
            //TODO: read from _waterSensor

            //adc_value = readadc(photo_ch, SPICLK, SPIMOSI, SPIMISO, SPICS)
            //if adc_value == 0:
            //         print"no water\n"
            //elif adc_value> 0 and adc_value<30 :
            //         print"it is raindrop\n"
            //elif adc_value>= 30 and adc_value<200 :
            //         print"it is water flow"
            //         print"water level:" + str("%.1f" % (adc_value / 200.* 100)) + "%\n"
            //#print "adc_value= " +str(adc_value)+"\n"
        }
    }
}
