namespace Movie.API.Requests
{
    public class AddCountryRequest
    {
        public string Name { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
    public class UpdateCountryRequest
    {
        public string Name { get; set; } = default!;
        /*public string Description { get; set; } = default!;*/
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = default!;
    }
    public class DeleteCountryRequest
    {

    }
}
