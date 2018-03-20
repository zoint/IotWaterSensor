/* 
    Author: Joshua Shapland (josh@madworldstudios.com)
    Adapted from Original Python Code: http://osoyoo.com/driver/waterlevel.py
    
    This meant to be a C# driver for the Kookye Water Level sensor for the Smart Home IOT Sensor Kit.     
*/
using Windows.Devices.Gpio;
using Windows.System.Threading;

namespace MadworldStudios.Kookye.Drivers
{
    public class WaterSensor 
    {
        //change these as desired - they're the pins connected from the
        //SPI port on the ADC to the Cobbler
        private const int SPICLK = 11;
        private const int SPIMISO = 9;
        private const int SPIMOSI = 10;
        private const int SPICS = 8;

        private GpioPin _spiclkPin;
        private GpioPin _spmisoPin;
        private GpioPin _spimosiPin;
        private GpioPin _spicsPin;
        
        //photoresistor connected to adc #0
        private const int photo_ch = 0;

        public WaterSensor()
        {
            InitGPIO();
        }
          
        private void InitGPIO()
        {
            //initialize the ports
            var controller = GpioController.GetDefault();
            
            //GPIO.setwarnings(False)
            //GPIO.cleanup()			#clean up at the end of your script
            //GPIO.setmode(GPIO.BCM)		#to specify whilch pin numbering system
            //set up the SPI interface pins

            //GPIO.setup(SPIMOSI, GPIO.OUT)
            _spimosiPin = controller.OpenPin(SPIMOSI);
            _spimosiPin.SetDriveMode(GpioPinDriveMode.Output);

            //GPIO.setup(SPIMISO, GPIO.IN)
            _spmisoPin = controller.OpenPin(SPIMISO);
            _spmisoPin.SetDriveMode(GpioPinDriveMode.Input);

            //GPIO.setup(SPICLK, GPIO.OUT)
            _spiclkPin = controller.OpenPin(SPICLK);
            _spiclkPin.SetDriveMode(GpioPinDriveMode.Output);

            //GPIO.setup(SPICS, GPIO.OUT)
            _spicsPin = controller.OpenPin(SPICS);
            _spicsPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public int ReadAdc(int adcNumb, int clockPin, int mosipin, int cspin)
        {
            //TODO: Finish converting this python code
            // CODE FROM waterlevel.py
            //def readadc(adcnum, clockpin, mosipin, misopin, cspin):
                //if ((adcnum > 7) or(adcnum < 0)):
                //        return -1
                //GPIO.output(cspin, True)

                //GPIO.output(clockpin, False)  # start clock low
                //GPIO.output(cspin, False)     # bring CS low

                //commandout = adcnum
                //commandout |= 0x18  # start bit + single-ended bit
                //commandout <<= 3    # we only need to send 5 bits here
                //for i in range(5):
                //        if (commandout & 0x80):
                //                GPIO.output(mosipin, True)
                //        else:
                //                GPIO.output(mosipin, False)
                //        commandout <<= 1
                //        GPIO.output(clockpin, True)
                //        GPIO.output(clockpin, False)

                //adcout = 0
                //# read in one empty bit, one null bit and 10 ADC bits
                //for i in range(12):
                //        GPIO.output(clockpin, True)
                //        GPIO.output(clockpin, False)
                //        adcout <<= 1
                //        if (GPIO.input(misopin)):
                //                adcout |= 0x1

                //GPIO.output(cspin, True)


                //adcout >>= 1       # first bit is 'null' so drop it
                //return adcout

            return 0;
        }
    }
}
