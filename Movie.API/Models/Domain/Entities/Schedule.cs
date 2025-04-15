using Movie.API.Models.Domain.Common;

namespace Movie.API.Models.Domain.Entities
{
    public class Schedule : BaseDomainEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public virtual ICollection<Film> Films { get; set; } = default!;
    }
}
