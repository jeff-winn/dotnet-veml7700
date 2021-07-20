using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers.v1
{
    [ApiController]
    [Route("api/v1/rain-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService sensorService;

        public SensorController(ISensorService sensorService)
        {            
            this.sensorService = sensorService;
        }

        [HttpGet("inspect")]
        public bool Get()
        {
            return sensorService.Inspect();
        }
    }
}
