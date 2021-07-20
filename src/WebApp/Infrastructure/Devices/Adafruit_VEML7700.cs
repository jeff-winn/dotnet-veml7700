using WebApp.Infrastructure.Primitives;

namespace WebApp.Infrastructure.Devices {
    /// <summary>
    /// Device driver for the Adafruit VEML7700 hardware.
    /// </summary>
    public class Adafruit_VEML7700 : IAdafruit_VEML7700 {
        private readonly II2cDevice device;

        #region Constants

        /// <summary>Light configuration register</summary>
        private const byte ALS_CONFIG = 0x00;
        /// <summary>Light high threshold for irq</summary>
        private const byte ALS_THRESHOLD_HIGH = 0x01;
        /// <summary>Light low threshold for irq</summary>
        private const byte ALS_THRESHOLD_LOW = 0x02;
        /// <summary>Power save register</summary>
        private const byte ALS_POWER_SAVE = 0x03;
        /// <summary>The light data output</summary>
        private const byte ALS_DATA = 0x04;

        /// <summary>The white light data output</summary>
        private const byte WHITE_DATA = 0x05;

        /// <summary>What IRQ (if any)</summary>
        private const byte INTERRUPT_STATUS = 0x06;

        /// <summary>Interrupt status for high threshold</summary>
        private const int INTERRUPT_HIGH = 0x4000;

        /// <summary>Interrupt status for low threshold</summary>
        private const int INTERRUPT_LOW = 0x8000;
        
        /// <summary>ALS irq persisance 1 sample</summary>
        private const byte PERS_1 = 0x00; 
        /// <summary>ALS irq persisance 2 samples</summary>
        private const byte PERS_2 = 0x01; 
        /// <summary>ALS irq persisance 4 samples</summary>
        private const byte PERS_4 = 0x02;
        /// <summary>ALS irq persisance 8 samples</summary>
        private const byte PERS_8 = 0x03;
        /// <summary>Power saving mode 1</summary>
        private const byte POWERSAVE_MODE1 = 0x00;
        /// <summary>Power saving mode 2</summary>
        private const byte POWERSAVE_MODE2 = 0x01;
        /// <summary>Power saving mode 3</summary>
        private const byte POWERSAVE_MODE3 = 0x02;
        /// <summary>Power saving mode 4</summary>
        private const byte POWERSAVE_MODE4 = 0x03;

        #endregion

        public bool IsEnabled { get; set; }

        public GainLevel Gain { get; set; }

        public IntegrationTime IntegrationTime { get; set; }

        public Adafruit_VEML7700(II2cDevice device) {
            this.device = device;
        }
    
        public void Init() {
        }

        public float ReadLux()
        {
            throw new System.NotImplementedException();
        }

        public float ReadLuxNormalized()
        {
            throw new System.NotImplementedException();
        }

        public short ReadAls()
        {
            throw new System.NotImplementedException();
        }

        public float ReadWhite()
        {
            throw new System.NotImplementedException();
        }

        public float ReadWhiteNormalized()
        {
            throw new System.NotImplementedException();
        }
    }
}