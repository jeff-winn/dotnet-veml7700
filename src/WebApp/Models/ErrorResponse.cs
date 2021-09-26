using System.Text.Json.Serialization;

namespace WebApp.Models {
    /// <summary>
    /// A response describing the error which occurred.
    /// </summary>
    public class ErrorResponse {
        /// <summary>
        /// The message describing the error.
        /// </summary>
        /// <example>An error occurred while processing the request.</example>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}