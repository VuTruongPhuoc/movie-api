namespace Movie.API.Requests
{
    public class AddFeedbackRequest
    {
        public int CommentId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateFeedbackRequest
    {
        public string Content { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
    }
    public class DeleteFeedbackRequest
    {

    }
}
