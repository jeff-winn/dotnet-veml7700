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
    [Route("api/v1/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly IInspectionService inspectionService;

        public SensorController(IInspectionService inspectionService)
        {            
            this.inspectionService = inspectionService;
        }

        /// <summary>
        /// Inspect device.
        /// </summary>
        /// <param name="deviceAddress">The I2C address of the device.</param>
        /// <remarks>
        /// Inspects a specific Adafruit VEML7700 device at the device address provided on the I2C bus.
        /// </remarks>
        [HttpGet("{deviceAddress}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The response successfully was able to read the light sensor data.", typeof(LuxResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was not valid.", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The light sensor requested was not found.", typeof(ErrorResponse))]
        public IActionResult Get(int deviceAddress)
        {
            try {
                var response = inspectionService.Inspect(deviceAddress);
                return Ok(response);
            }
            catch (BadRequestException ex) {
                return BadRequest(new ErrorResponse {
                    ErrorMessage = ex.Message
                });
            }
            catch (DeviceNotFoundException) {
                return NotFound(new ErrorResponse {
                    ErrorMessage = $"The sensor at device address {deviceAddress} does not exist."
                });
            }
        }
    }
}