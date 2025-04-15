namespace Movie.API.Requests
{
    public class AddTrackRequest
    {
        public string UserId { get; set; } = default!;
        public int FilmId { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class DeleteTrackRequest
    {

    }
}
