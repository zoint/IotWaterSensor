/* 
    Author: Joshua Shapland (josh@madworldstudios.com)
    Adapted from Original Python Code: http://osoyoo.com/driver/waterlevel.py
    
    This meant to be a C# driver for the Kookye Water Level sensor for the Smart Home IOT Sensor Kit.     
*/
using Windows.Devices.Gpio;

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

        //photoresistor connected to adc #0
        private const int PHOTO_CH = 0;

        private GpioPin _spiclkPin;
        private GpioPin _spmisoPin;
        private GpioPin _spimosiPin;
        private GpioPin _spicsPin;      
        
        public WaterSensor()
        {
            InitGPIO();
        }
          
        private void InitGPIO()
        {
            //initialize the ports
            var controller = GpioController.GetDefault();

            //TODO: Figure it if I need to implement these in C#? - Don't think so as of now.
            //GPIO.setwarnings(False)
            //GPIO.cleanup()			#clean up at the end of your script
            //GPIO.setmode(GPIO.BCM)		#to specify whilch pin numbering system

            //set up the SPI interface pins
            _spimosiPin = controller.OpenPin(SPIMOSI);//GPIO.setup(SPIMOSI, GPIO.OUT)
            _spimosiPin.SetDriveMode(GpioPinDriveMode.Output);

            _spmisoPin = controller.OpenPin(SPIMISO); //GPIO.setup(SPIMISO, GPIO.IN)
            _spmisoPin.SetDriveMode(GpioPinDriveMode.Input);

            _spiclkPin = controller.OpenPin(SPICLK); //GPIO.setup(SPICLK, GPIO.OUT)
            _spiclkPin.SetDriveMode(GpioPinDriveMode.Output);

            _spicsPin = controller.OpenPin(SPICS); //GPIO.setup(SPICS, GPIO.OUT)
            _spicsPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public double GetWaterLevelPercentage()
        {
            var adcValue = ReadAdc();
            return (adcValue / 200) * 100;
        }

        private int ReadAdc() //def readadc(adcnum, clockpin, mosipin, misopin, cspin):
        {
            if(PHOTO_CH > 7 || PHOTO_CH < 0) //if ((adcnum > 7) or(adcnum < 0)):
            {
                return -1;
            }

            //GPIO.output(cspin, True)
            _spicsPin.Write(GpioPinValue.High);
            //GPIO.output(clockpin, False)  # start clock low
            _spiclkPin.Write(GpioPinValue.Low);
            //GPIO.output(cspin, False)     # bring CS low
            _spicsPin.Write(GpioPinValue.Low);            
            
            var commandout = PHOTO_CH;  //commandout = adcnum
            commandout |= 0x18; //commandout |= 0x18  # start bit + single-ended bit
            commandout <<= 3; //commandout <<= 3    # we only need to send 5 bits here

            for (int i = 0; i < 5; i++)  //for i in range(5):
            {
                
                if (commandout < 768 && commandout != 0) //if (commandout & 0x80):    
                    _spimosiPin.Write(GpioPinValue.High);  //GPIO.output(mosipin, True)         
                else  // else:
                    _spimosiPin.Write(GpioPinValue.Low); //  GPIO.output(mosipin, False)

                commandout <<= 1; //commandout <<= 1

                _spiclkPin.Write(GpioPinValue.High);  //GPIO.output(clockpin, True)
                _spiclkPin.Write(GpioPinValue.Low);  //GPIO.output(clockpin, False)
            }

            var adcout = 0; //adcout = 0

            //# read in one empty bit, one null bit and 10 ADC bits
            for (int i = 0; i < 12; i++) //for i in range(12):
            {   //GPIO.output(clockpin, True)
                _spiclkPin.Write(GpioPinValue.High);
                //GPIO.output(clockpin, False)
                _spiclkPin.Write(GpioPinValue.Low);
                adcout <<= 1;
                
                if (_spmisoPin.Read() == GpioPinValue.High) //if (GPIO.input(misopin)):
                    adcout |= 0x1;
            }

            _spicsPin.Write(GpioPinValue.High); //GPIO.output(cspin, True)

            adcout >>= 1; //first bit is 'null' so drop it
            return adcout; //return adcout
        }
    }
}
