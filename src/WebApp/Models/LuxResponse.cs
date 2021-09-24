using System.Text.Json.Serialization;

namespace  WebApp.Models
{
    public class LuxResponse {
        [JsonPropertyName("lux")]
        public double Lux { get; set; }
        [JsonPropertyName("whiteLight")]
        public double WhiteLight { get; set; }
    }
}