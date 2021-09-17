// https://github.com/adafruit/Adafruit_VEML7700
// https://github.com/adafruit/Adafruit_BusIO/blob/master/Adafruit_BusIO_Register.h
// https://learn.adafruit.com/adafruit-veml7700/arduino

namespace WebApp.Infrastructure.Devices {
    public enum GainLevel : byte { 
        Level1 = 0,
        Level2 = 1,
        Level1_8 = 2,
        Level1_4 = 3
    }

    public enum IntegrationTime : byte {
        IT_100MS = 0,
        IT_200MS = 1,
        IT_400MS = 2,
        IT_800MS = 3,
        IT_50MS = 8,
        IT_25MS = 12
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
        float ReadWhite();
    }
}