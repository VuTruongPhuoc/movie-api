namespace Movie.API.Requests
{
    public class AddEpisodeRequest
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Link { get; set; } = default!;
        public int FilmId { get; set; } = default!;
        public int? SectionId { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateEpisodeRequest
    {
        public int FilmId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Link { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
    }
    public class DeleteEpisodeRequest
    {

    }
}
