using System;
using System.Collections;
using System.Device.I2c;
using Adafruit.Devices.Primitives;

#pragma warning disable CS8618 // Guaranteed to be set prior to use via required call to initialize.

namespace Adafruit.Devices.Veml7700
{
    /// <summary>
    /// A fake device driver for the Adafruit VEML7700 hardware.
    /// </summary>
    public class FakeAdafruit_VEML7700 : IAdafruit_VEML7700
    {
        private readonly ushort readLuxValue;
        private readonly float readLuxNormalizedValue;
        private readonly ushort readWhiteValue;
        private readonly float readWhiteNormalizedValue;

        public FakeAdafruit_VEML7700(ushort readLuxValue = 0, float readLuxNormalizedValue = 0.0F, ushort readWhiteValue = 0, float readWhiteNormalizedValue = 0.0F) {
            this.readLuxValue = readLuxValue;
            this.readLuxNormalizedValue = readLuxNormalizedValue;
            this.readWhiteValue = readWhiteValue;
            this.readWhiteNormalizedValue = readWhiteNormalizedValue;
        }

        public bool IsEnabled { get; set; }
        public GainLevel Gain { get; set; }
        public IntegrationTime IntegrationTime { get; set; }

        public void Init()
        {
            // This method intentionally left blank.
        }

        public ushort ReadLux()
        {
            return readLuxValue;
        }

        public float ReadLuxNormalized()
        {
            return readLuxNormalizedValue;
        }

        public ushort ReadWhite()
        {
            return readWhiteValue;
        }

        public float ReadWhiteNormalized()
        {
            return readWhiteNormalizedValue;
        }
    }
}