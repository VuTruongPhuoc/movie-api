namespace Movie.API.Models.Domain.Entities
{
    public class FilmCategory
    {
        public int FilmId { get; set; } = default!;
        public int CategoryId { get; set; } = default!;
        public Film? Film { get; set; } = default!;
        public Category? Category { get; set; } = default!;
    }
}
