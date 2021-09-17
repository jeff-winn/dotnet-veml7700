// https://github.com/adafruit/Adafruit_VEML7700
// https://github.com/adafruit/Adafruit_BusIO/blob/master/Adafruit_BusIO_Register.h
// https://learn.adafruit.com/adafruit-veml7700/arduino

namespace Adafruit.Devices.Veml7700
{
    /// <summary>
    /// Identifies a device driver for the Adafruit VEML7700.
    /// </summary>
    public interface IAdafruit_VEML7700 { 
        bool IsEnabled { get; set; }
        GainLevel Gain { get; set; }
        IntegrationTime IntegrationTime { get; set; }

        void Init();

        ushort ReadLux();
        float ReadLuxNormalized();
        ushort ReadWhite();
        float ReadWhiteNormalized();
    }
}