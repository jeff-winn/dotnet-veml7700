using Microsoft.AspNetCore.Mvc;
using WebApp.Exceptions;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers.v1
{
    [ApiController]
    [Route("api/v1/light-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IInspectionService inspectionService;

        public SensorController(IInspectionService inspectionService)
        {            
            this.inspectionService = inspectionService;
        }

        [HttpGet("inspect/{deviceId}")]
        public IActionResult Get(int deviceId)
        {
            try {
                var response = inspectionService.Inspect(deviceId);
                return Ok(response);
            }
            catch (BadRequestException ex) {
                return BadRequest(new ErrorResponse {
                    ErrorMessage = ex.Message
                });
            }
            catch (DeviceNotFoundException) {
                return NotFound();
            }
        }
    }
}