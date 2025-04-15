namespace Movie.API.Requests
{
    public class AddCategoryRequest
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateCategoryRequest
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
    }
    public class DeleteCategoryRequest
    {

    }
}
