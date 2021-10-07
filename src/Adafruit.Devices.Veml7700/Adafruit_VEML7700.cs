using System;
using System.Collections;
using System.Device.I2c;
using Adafruit.Devices.Primitives;
using Microsoft.Extensions.Logging;

#pragma warning disable CS8618 // Guaranteed to be set prior to use via required call to initialize.

namespace Adafruit.Devices.Veml7700
{
    /// <summary>
    /// Device driver for the Adafruit VEML7700 hardware.
    /// </summary>
    public class Adafruit_VEML7700 : IAdafruit_VEML7700
    {
        private readonly II2cDevice device;
        private readonly ILogger<Adafruit_VEML7700> logger;

        private II2cRegister configRegister;
        private II2cRegister dataRegister;
        private II2cRegister whiteDataRegister;
        private II2cRegisterBits shutdownBits;
        private II2cRegisterBits integrationTimeBits;
        private II2cRegisterBits gainBits;

        private GainLevel gain;
        private IntegrationTime integrationTime;
        private float luxMultiplier;
        private bool isEnabled;
        private bool initialized;

        #region Constants

        /// <summary>Light configuration register</summary>
        private const byte ALS_CONF_0 = 0x00;
        /// <summary>The light data output</summary>
        private const byte ALS = 0x04;
        /// <summary>The white light data output</summary>
        private const byte WHITE = 0x05;

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

        public GainLevel Gain 
        {
            get { return gain; }
            set {
                GuardMustBeInitialized();

                gain = value;
                gainBits.Write((byte)value);

                AdjustLuxMultiplier();
            }
        }

        public IntegrationTime IntegrationTime 
        {
            get { return integrationTime; }
            set {
                GuardMustBeInitialized();

                integrationTime = value;
                integrationTimeBits.Write((byte)value);

                AdjustLuxMultiplier();            
            }
        }

        public Adafruit_VEML7700(II2cDevice device, ILogger<Adafruit_VEML7700> logger)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ~Adafruit_VEML7700() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                device.Dispose();
            }
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
            dataRegister = new I2cRegister(device, ALS);
            whiteDataRegister = new I2cRegister(device, WHITE);

            shutdownBits = configRegister.GetRegisterBits(0, 1);
            integrationTimeBits = configRegister.GetRegisterBits(6, 4);
            gainBits = configRegister.GetRegisterBits(11, 2);

            initialized = true;
        }

        private void GuardMustBeInitialized()
        {
            if (!initialized)
            {
                throw new NotSupportedException("This instance has not yet been initialized.");
            }
        }

        public ushort ReadLux()
        {
            return dataRegister.ReadUInt16();
        }
        
        public float ReadLuxNormalized() 
        {
            var lux = ReadLux();
            var result = lux * luxMultiplier;

            logger.LogTrace($"LuxNormalized: {result} = {lux} * {luxMultiplier}");
            return result;
        }

        public ushort ReadWhite()
        {
            return whiteDataRegister.ReadUInt16();
        }

        public float ReadWhiteNormalized()
        {
            var white = ReadWhite();
            var result = white * luxMultiplier;

            logger.LogTrace($"WhiteNormalized: {result} = {white} * {luxMultiplier}");
            return result;
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
