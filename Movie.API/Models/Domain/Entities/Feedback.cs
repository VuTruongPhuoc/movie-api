using Movie.API.Models.Domain.Common;

namespace Movie.API.Models.Domain.Entities
{
    public class Feedback : BaseDomainEntity
    {
        public int CommentId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public bool IsLocked { get; set; } = default!;
        public User? User { get; set; } = default!;
        public Comment? Comment { get; set; } = default!;
    }
}
