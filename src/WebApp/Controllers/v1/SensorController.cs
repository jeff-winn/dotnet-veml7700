using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Infrastructure.Primitives;

namespace WebApp.Controllers.v1
{
    [ApiController]
    [Route("api/v1/rain-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IGpioController gpio;
        private readonly ILogger<SensorController> logger;

        public SensorController(IGpioController gpio, ILogger<SensorController> logger)
        {
            this.gpio = gpio;
            this.logger = logger;
        }

        [HttpGet("inspect")]
        public bool Get()
        {
            return true;
        }
    }
}
