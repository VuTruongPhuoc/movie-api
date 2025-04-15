namespace Movie.API.Responses.DTOs
{
    public class SectionDTO
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = default!;
    }
}
