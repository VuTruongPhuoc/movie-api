namespace Movie.API.Requests
{
    public class AddCommentRequest
    {
        public int FilmId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateCommentRequest
    {
        public string Content { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
    }
    public class DeleteCommentRequest
    {

    }
}
