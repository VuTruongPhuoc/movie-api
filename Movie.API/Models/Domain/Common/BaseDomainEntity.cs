

namespace Movie.API.Models.Domain.Common
{
    public class BaseDomainEntity
    {
        public int Id { get; set; } = default!;
        public DateTime CreateDate { get; set; } = default!;
        public DateTime LastModifiedDate { get; set; } = default!;
        public bool IsActive { get; set; } = default!;
        public int Status { get; set; } = default!;


    }
}
