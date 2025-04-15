namespace Movie.API.Responses.DTOs
{
    public class EpisodeDTO
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string FilmName { get; set; } = default!;
        public string SectionName { get; set; } = default!;
        public string Link { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = default!;
    }
}
