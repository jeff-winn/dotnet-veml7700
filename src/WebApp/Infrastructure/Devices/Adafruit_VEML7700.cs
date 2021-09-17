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

        protected IAdafruit_I2CRegister config;
        // protected IAdafruit_I2CRegister highThreshold;
        // protected IAdafruit_I2CRegister lowThreshold;
        // protected IAdafruit_I2CRegister powerSaving;
        // protected IAdafruit_I2CRegister data;
        // protected IAdafruit_I2CRegister whiteData;
        // protected IAdafruit_I2CRegister interruptStatus;

        protected IAdafruit_I2CRegisterBits shutdown;
        // protected IAdafruit_I2CRegisterBits interruptEnable;
        // protected IAdafruit_I2CRegisterBits persistence;
        // protected IAdafruit_I2CRegisterBits integrationTime;
        // protected IAdafruit_I2CRegisterBits gain;
        // protected IAdafruit_I2CRegisterBits powerSaveEnable;
        // protected IAdafruit_I2CRegisterBits powerSaveMode;

        private bool initialized = false;

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

        public bool IsEnabled
        {
            get
            {
                GuardMustBeInitialized();

                // The device has the bit flipped for enabled/disabled, so this is intentional.
                return !shutdown.ReadBool();
            }
            set
            {
                GuardMustBeInitialized();

                // The device has the bit flipped for enabled/disabled, so this is intentional.
                shutdown.Write(!value);
            }
        }

        public GainLevel Gain { get; set; }

        public IntegrationTime IntegrationTime { get; set; }

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
            initialized = true;
        }

        protected virtual void InitializeCore()
        {
            config = new Adafruit_I2CRegister(device, ALS_CONFIG);
            // highThreshold = new Adafruit_I2CRegister(device, ALS_THRESHOLD_HIGH);
            // lowThreshold = new Adafruit_I2CRegister(device, ALS_THRESHOLD_LOW);
            // powerSaving = new Adafruit_I2CRegister(device, ALS_POWER_SAVE);
            // data = new Adafruit_I2CRegister(device, ALS_DATA);
            // whiteData = new Adafruit_I2CRegister(device, WHITE_DATA);
            // interruptStatus = new Adafruit_I2CRegister(device, INTERRUPT_STATUS);

            shutdown = config.GetRegisterBits(0, 1);
            // interruptEnable = config.GetRegisterBits(1, 1);
            // persistence = config.GetRegisterBits(4, 2);
            // integrationTime = config.GetRegisterBits(6, 4);
            // gain = config.GetRegisterBits(11, 2);
            // powerSaveEnable = powerSaving.GetRegisterBits(0, 1);
            // powerSaveMode = powerSaving.GetRegisterBits(1, 2);
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