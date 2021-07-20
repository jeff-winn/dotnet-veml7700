// https://github.com/adafruit/Adafruit_VEML7700
// https://github.com/adafruit/Adafruit_BusIO/blob/master/Adafruit_BusIO_Register.h
// https://learn.adafruit.com/adafruit-veml7700/arduino

namespace WebApp.Infrastructure.Devices {
    public enum GainLevel : byte { 
        Level1 = 0x00,
        Level2 = 0x01,
        Level1_8 = 0x02,
        Level1_4 = 0x03
    }

    public enum IntegrationTime : byte {        
        IT_100MS = 0x00,
        IT_200MS = 0x01,
        IT_400MS = 0x02,
        IT_800MS = 0x03,
        IT_50MS = 0x08,
        IT_25MS = 0x0C
    }

    /// <summary>
    /// Identifies a device driver for the Adafruit VEML7700.
    /// </summary>
    public interface IAdafruit_VEML7700 { 
        bool IsEnabled { get; set; }
        GainLevel Gain { get; set; }
        IntegrationTime IntegrationTime { get; set; }

        void Init();

        float ReadLux();
        float ReadLuxNormalized();

        short ReadAls();
        float ReadWhite();
        float ReadWhiteNormalized();
    }
}