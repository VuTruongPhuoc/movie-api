using Movie.API.Models.Domain.Entities;
using System.Text.Json.Serialization;

namespace Movie.API.Responses.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; } = default!;
        [JsonIgnore]
        public string UserId { get; set; } = default!;
        [JsonIgnore]
        public string FilmId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public UserDTO User { get; set; } = default!;
        public List<FeedbackDTO> Feedbacks { get; set; } = default!;

    }
}
