using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers.v1
{
    [ApiController]
    [Route("api/v1/rain-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IInspectionService inspectionService;

        public SensorController(IInspectionService inspectionService)
        {            
            this.inspectionService = inspectionService;
        }

        [HttpGet("inspect")]
        public bool Get()
        {
            return inspectionService.Inspect();
        }
    }
}
