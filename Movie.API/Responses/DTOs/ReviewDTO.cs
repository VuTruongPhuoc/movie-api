namespace Movie.API.Responses.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string FilmId { get; set; } = default!;
        public int Rate { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
    }

    public class ReviewTotal
    {
        public int Count { get; set; } = default!;
        public double AvgRate { get; set; } = default!;
    }
}
