using System;
using System.Collections;
using WebApp.Infrastructure.Primitives;

#pragma warning disable CS8618 // Guaranteed to be set prior to use via required call to initialize.

namespace WebApp.Infrastructure.Devices
{
    /// <summary>
    /// Device driver for the Adafruit VEML7700 hardware.
    /// </summary>
    public class Adafruit_VEML7700 : IAdafruit_VEML7700
    {
        private readonly II2cDevice device;

        private IAdafruit_I2CRegister configRegister;
        private IAdafruit_I2CRegister highThresholdRegister;
        private IAdafruit_I2CRegister lowThresholdRegister;
        private IAdafruit_I2CRegister powerSavingRegister;
        private IAdafruit_I2CRegister dataRegister;
        private IAdafruit_I2CRegister whiteDataRegister;
        private IAdafruit_I2CRegister interruptStatusRegister;
        private IAdafruit_I2CRegisterBits shutdownBits;
        private IAdafruit_I2CRegisterBits interruptEnableBits;
        private IAdafruit_I2CRegisterBits persistenceBits;
        private IAdafruit_I2CRegisterBits integrationTimeBits;
        private IAdafruit_I2CRegisterBits gainBits;
        private IAdafruit_I2CRegisterBits powerSaveEnableBits;
        private IAdafruit_I2CRegisterBits powerSaveModeBits;

        private float luxMultiplier;
        private GainLevel gain;
        private IntegrationTime integrationTime;

        private bool initialized;

        #region Constants

        /// <summary>Light configuration register</summary>
        private const byte ALS_CONF_0 = 0x00;
        /// <summary>Light high threshold for irq</summary>
        private const byte ALS_WH = 0x01;
        /// <summary>Light low threshold for irq</summary>
        private const byte ALS_WL = 0x02;
        /// <summary>Power save register</summary>
        private const byte ALS_POWER_SAVE = 0x03;
        /// <summary>The light data output</summary>
        private const byte ALS = 0x04;

        /// <summary>The white light data output</summary>
        private const byte WHITE = 0x05;

        /// <summary>What IRQ (if any)</summary>
        private const byte ALS_INT = 0x06;

        // /// <summary>Interrupt status for high threshold</summary>
        // private const int INTERRUPT_HIGH = 0x4000;

        // /// <summary>Interrupt status for low threshold</summary>
        // private const int INTERRUPT_LOW = 0x8000;

        /// <summary>ALS irq persisance 1 sample</summary>
        private const byte ALS_PERS_1 = 0x00;
        /// <summary>ALS irq persisance 2 samples</summary>
        private const byte ALS_PERS_2 = 0x01;
        /// <summary>ALS irq persisance 4 samples</summary>
        private const byte ALS_PERS_4 = 0x10;
        /// <summary>ALS irq persisance 8 samples</summary>
        private const byte ALS_PERS_8 = 0x11;
        /// <summary>Power saving mode 1</summary>
        private const byte PSM_1 = 0;
        /// <summary>Power saving mode 2</summary>
        private const byte PSM_2 = 1;
        /// <summary>Power saving mode 3</summary>
        private const byte PSM_3 = 2;
        /// <summary>Power saving mode 4</summary>
        private const byte PSM_4 = 3;

        #endregion

        public bool IsEnabled
        {
            get
            {
                GuardMustBeInitialized();

                // The device has the bit flipped for enabled/disabled, so this is intentional.
                return !shutdownBits.ReadBool();
            }
            set
            {
                GuardMustBeInitialized();

                // The device has the bit flipped for enabled/disabled, so this is intentional.
                shutdownBits.Write(!value);
            }
        }

        public bool IsInterruptEnabled 
        {
            get 
            {
                GuardMustBeInitialized();

                return interruptEnableBits.ReadBool();
            }
            set 
            {
                GuardMustBeInitialized();

                interruptEnableBits.Write(value);                
            }
        }

        public GainLevel Gain 
        {
            get {
                return gain;
            }
            set {                
                gain = value;
                AdjustLuxMultiplier();
            }
        }

        public IntegrationTime IntegrationTime 
        {
            get {
                return integrationTime;
            }
            set {
                integrationTime = value;
                AdjustLuxMultiplier();            
            }
        }

        public Adafruit_VEML7700(II2cDevice device)
        {
            this.device = device;
        }

        public void Init()
        {
            if (initialized)
            {
                throw new NotSupportedException("This instance has already been initialized.");
            }

            InitializeCore();

            IsEnabled = false;
            IsInterruptEnabled = false;
            Gain = GainLevel.Level1;
            IntegrationTime = IntegrationTime.IT_100MS;
        }

        protected virtual void InitializeCore()
        {
            configRegister = new Adafruit_I2CRegister(device, ALS_CONF_0);
            highThresholdRegister = new Adafruit_I2CRegister(device, ALS_WH);
            lowThresholdRegister = new Adafruit_I2CRegister(device, ALS_WL);
            powerSavingRegister = new Adafruit_I2CRegister(device, ALS_POWER_SAVE);
            dataRegister = new Adafruit_I2CRegister(device, ALS);
            whiteDataRegister = new Adafruit_I2CRegister(device, WHITE);
            interruptStatusRegister = new Adafruit_I2CRegister(device, ALS_INT);

            shutdownBits = configRegister.GetRegisterBits(0, 1);
            interruptEnableBits = configRegister.GetRegisterBits(1, 1);
            persistenceBits = configRegister.GetRegisterBits(4, 2);
            integrationTimeBits = configRegister.GetRegisterBits(6, 4);
            gainBits = configRegister.GetRegisterBits(11, 2);
            powerSaveEnableBits = configRegister.GetRegisterBits(0, 1);
            powerSaveModeBits = configRegister.GetRegisterBits(1, 2);

            initialized = true;
        }

        private void GuardMustBeInitialized()
        {
            if (!initialized)
            {
                throw new NotSupportedException("This instance has not yet been initialized.");
            }
        }

        public float ReadLux()
        {
            var raw = dataRegister.ReadUInt16();        
            return raw * luxMultiplier;
        }

        public float ReadWhite()
        {
            throw new System.NotImplementedException();
        }

        private void AdjustLuxMultiplier() 
        {
            var multiplier = 0.0036F;

            switch (integrationTime) {
                case IntegrationTime.IT_400MS:
                    multiplier *= 2;
                    break;

                case IntegrationTime.IT_200MS:
                    multiplier *= 4;
                    break;

                case IntegrationTime.IT_100MS:
                    multiplier *= 8;
                    break;

                case IntegrationTime.IT_50MS:
                    multiplier *= 16;
                    break;

                case IntegrationTime.IT_25MS:
                    multiplier *= 32;
                    break;
            }

            switch (gain) {
                case GainLevel.Level1:
                    multiplier *= 2;
                    break;

                case GainLevel.Level1_4:
                    multiplier *= 8;
                    break;

                case GainLevel.Level1_8:
                    multiplier *= 16;
                    break;
            }

            luxMultiplier = multiplier;
        }
    }
}
