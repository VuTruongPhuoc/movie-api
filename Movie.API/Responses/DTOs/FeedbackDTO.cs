using System.Text.Json.Serialization;

namespace Movie.API.Responses.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; } = default!;
        [JsonIgnore]
        public string UserId { get; set; } = default!;
        [JsonIgnore]
        public string CommentId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public UserDTO User { get; set; } = default!;
    }
}
