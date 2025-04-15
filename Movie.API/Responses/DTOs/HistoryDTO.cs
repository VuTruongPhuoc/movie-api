namespace Movie.API.Responses.DTOs
{
    public class HistoryDTO
    {
        public int Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string FilmId { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
    }
}
