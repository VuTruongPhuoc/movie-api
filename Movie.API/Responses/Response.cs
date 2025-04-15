using System.Net;
using System.Text.Json.Serialization;

namespace Movie.API.Responses
{

    public class Response
    {
        [JsonPropertyOrder(-1)]
        public bool Success { get; set; }
        [JsonPropertyOrder(-1)]
        public HttpStatusCode StatusCode { get; set; } = default!;
        [JsonPropertyOrder(-1)]
        public string Message { get; set; } = default!;
    }
}
