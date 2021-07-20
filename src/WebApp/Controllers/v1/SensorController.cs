using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Devices;

namespace WebApp.Controllers.v1
{
    [ApiController]
    [Route("api/v1/rain-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IAdafruit_VEML7700 sensor;

        public SensorController(IAdafruit_VEML7700 sensor)
        {            
            this.sensor = sensor;
        }

        [HttpGet("inspect")]
        public bool Get()
        {
            return true;
        }
    }
}
