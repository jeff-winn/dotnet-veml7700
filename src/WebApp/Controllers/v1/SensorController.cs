using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
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
        public LuxResponse Get()
        {
            return inspectionService.Inspect();
        }
    }
}
