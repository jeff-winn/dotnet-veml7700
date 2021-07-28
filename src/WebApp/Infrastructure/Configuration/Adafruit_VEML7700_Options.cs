using WebApp.Infrastructure.Devices;

namespace WebApp.Infrastructure.Configuration {
    public class Adafruit_VEML7700_Options { 
        public int DeviceAddress { get; set; }
        public int BusId { get; set; }
        public GainLevel Gain { get; set; }
        public IntegrationTime IntegrationTime { get; set; }
    }
}