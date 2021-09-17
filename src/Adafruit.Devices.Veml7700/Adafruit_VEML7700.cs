using System;
using System.Collections;
using System.Device.I2c;
using Adafruit.Devices.Primitives;

#pragma warning disable CS8618 // Guaranteed to be set prior to use via required call to initialize.

namespace Adafruit.Devices.Veml7700
{
    /// <summary>
    /// Device driver for the Adafruit VEML7700 hardware.
    /// </summary>
    public class Adafruit_VEML7700 : IAdafruit_VEML7700
    {
        private readonly II2cDevice device;

        private II2cRegister configRegister;
        private II2cRegister highThresholdRegister;
        private II2cRegister lowThresholdRegister;
        private II2cRegister powerSavingRegister;
        private II2cRegister dataRegister;
        private II2cRegister whiteDataRegister;
        private II2cRegister interruptStatusRegister;
        private II2cRegisterBits shutdownBits;
        private II2cRegisterBits interruptEnableBits;
        private II2cRegisterBits persistenceBits;
        private II2cRegisterBits integrationTimeBits;
        private II2cRegisterBits gainBits;
        private II2cRegisterBits powerSaveEnableBits;
        private II2cRegisterBits powerSaveModeBits;

        private GainLevel gain;
        private IntegrationTime integrationTime;
        private float luxMultiplier;
        private bool isEnabled;
        private bool initialized;
        private bool isInterruptEnabled;

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
            get { return isEnabled; }
            set
            {
                GuardMustBeInitialized();

                isEnabled = value;
                shutdownBits.Write(!value);
            }
        }

        public bool IsInterruptEnabled 
        {
            get { return isInterruptEnabled; }
            set 
            {
                GuardMustBeInitialized();
                
                isInterruptEnabled = value;
                interruptEnableBits.Write(value);                
            }
        }

        public GainLevel Gain 
        {
            get { return gain; }
            set {
                gain = value;
                gainBits.Write((byte)value);

                AdjustLuxMultiplier();
            }
        }

        public IntegrationTime IntegrationTime 
        {
            get { return integrationTime; }
            set {
                integrationTime = value;
                integrationTimeBits.Write((byte)value);

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

            isEnabled = !shutdownBits.ReadBool();
            gain = (GainLevel)gainBits.Read();
            integrationTime = (IntegrationTime)integrationTimeBits.Read();

            AdjustLuxMultiplier();
        }

        protected virtual void InitializeCore()
        {
            configRegister = new I2cRegister(device, ALS_CONF_0);
            highThresholdRegister = new I2cRegister(device, ALS_WH);
            lowThresholdRegister = new I2cRegister(device, ALS_WL);
            powerSavingRegister = new I2cRegister(device, ALS_POWER_SAVE);
            dataRegister = new I2cRegister(device, ALS);
            whiteDataRegister = new I2cRegister(device, WHITE);
            interruptStatusRegister = new I2cRegister(device, ALS_INT);

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

            switch (IntegrationTime) {
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

            switch (Gain) {
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
