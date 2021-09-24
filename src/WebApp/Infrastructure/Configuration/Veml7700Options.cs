using Adafruit.Devices.Veml7700;

namespace WebApp.Infrastructure.Configuration
{
    public class Veml7700Options
    {
        public int BusId { get; set; }
        public GainLevel Gain { get; set; }
        public IntegrationTime IntegrationTime { get; set; }
    }
}