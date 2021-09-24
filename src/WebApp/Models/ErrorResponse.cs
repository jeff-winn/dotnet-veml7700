using System.Text.Json.Serialization;

namespace WebApp.Models {
    public class ErrorResponse {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}