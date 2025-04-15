namespace Movie.API.Responses.DTOs
{
    public class FilmDTO
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public string Poster { get; set; } = default!;
        public string? PosterUrl { get; set; } = default!;
        public string OriginName { get; set; } = default!;
        public string Time { get; set; } = default!;
        public string Type { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int NumberOfEpisodes { get; set; } = default!;
        public string Trailer { get; set; } = default!;
        public List<CategoryDTO> Categories { get; set; } = default!;
        public CountryDTO Country { get; set; } = default!;
        public ScheduleDTO Schedule { get; set; } = default!;
        public ReviewTotal Review { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; } = default!;
    }

    public class FilmFilter
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public string Poster { get; set; } = default!;
        public string? PosterUrl { get; set; } = default!;
        public string OriginName { get; set; } = default!;
        public string Time { get; set; } = default!;
        public string Type { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int NumberOfEpisodes { get; set; } = default!;
    }

    public class FilmImage
    {
        public int Id { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;

    }
    public class FilmPoster
    {
        public int Id { get; set; } = default!;
        public string? Poster { get; set; } = default!;
        public string? PosterUrl { get; set; } = default!;

    }
}
