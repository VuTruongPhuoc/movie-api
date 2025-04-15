namespace Movie.API.Requests
{
    public class AddReviewRequest
    {

        public int FilmId { get; set; } = default!;
        public int Rate { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateReviewRequest
    {

        public int Rate { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class DeleteReviewRequest
    {

    }
}
