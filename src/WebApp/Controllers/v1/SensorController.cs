using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Exceptions;
using WebApp.Models;
using WebApp.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Controllers.v1
{
    /// <summary>
    /// Interacts with the sensor hardware.
    /// </summary>
    [ApiController]
    [Route("api/v1/light-sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IInspectionService inspectionService;

        /// <summary>
        /// Initializes an instance of the <see cref="SensorController" /> class.
        /// </summary>
        /// <param name="inspectionService">The inspection service.</param>
        public SensorController(IInspectionService inspectionService)
        {            
            this.inspectionService = inspectionService;
        }

        /// <summary>
        /// Inspect device.
        /// </summary>
        /// <remarks>
        /// Inspects a specific Adafruit VEML7700 device at the device id address provided on the I2C bus.
        /// </remarks>
        /// <example>inspect/16</example>
        [HttpGet("inspect/{deviceId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The response successfully was able to read the light sensor data.", typeof(LuxResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was not valid.", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The light sensor requested was not found.", typeof(ErrorResponse))]
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