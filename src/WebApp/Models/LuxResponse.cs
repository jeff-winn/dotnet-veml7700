using System.Text.Json.Serialization;

namespace WebApp.Models
{
    /// <summary>
    /// A response describing the light sensor data.
    /// </summary>
    public class LuxResponse {
        /// <summary>
        /// The lux data.
        /// </summary>
        /// <example>250.48129</example>
        [JsonPropertyName("lux")]
        public double Lux { get; set; }

        /// <summary>
        /// The white light data.
        /// </summary>
        /// <example>284.61</example>   
        [JsonPropertyName("whiteLight")]
        public double WhiteLight { get; set; }
    }
}