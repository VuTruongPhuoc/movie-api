namespace Movie.API.Responses.DTOs
{
    public class ScheduleDTO
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = default!;
    }
}
