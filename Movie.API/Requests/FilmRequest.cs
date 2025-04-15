using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.API.Requests
{
    public class AddFilmRequest
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string OriginName { get; set; } = default!;
        public string Time { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int NumberOfEpisodes { get; set; } = default!;
        public string Trailer { get; set; } = default!;
        public int CountryId { get; set; } = default!;
        public int ScheduleId { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public List<int> CategoryIds { get; set; } = default!;
    }
    public class UpdateFilmRequest
    {
        public string Name { get; set; } = default!;
        public string Slug { set; get; } = default!;
        public string Description { get; set; } = default!;
        public string OriginName { get; set; } = default!;
        public string Time { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int NumberOfEpisodes { get; set; } = default!;
        public string Trailer { get; set; } = default!;
        public int CountryId { get; set; } = default!;
        public int ScheduleId { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
        public List<int> CategoryIds { get; set; } = default!;
    }
    public class DeleteFilmRequest
    {

    }
    public class ChangeFilmImageRequest
    {
        public int Id { get; set; } = default!;
        [NotMapped]
        public IFormFile ImageFile { get; set; } = default!;
    }
    public class ChangeFilmPosterRequest
    {
        public int Id { get; set; } = default!;
        [NotMapped]
        public IFormFile PosterFile { get; set; } = default!;
    }
}
