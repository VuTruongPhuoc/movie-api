using System.Text.Json.Serialization;

namespace Movie.API.Responses
{
    public class DataRespone : Response
    {
        [JsonPropertyOrder(1)]
        public dynamic Data { get; set; } = default!;
    }
}
