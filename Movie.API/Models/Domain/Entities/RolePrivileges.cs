using Movie.API.Models.Domain.Common;

namespace Movie.API.Models.Domain.Entities
{
    public class RolePrivileges : BaseDomainEntity
    {
        public string RoleId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Role? Role { get; set; } = default!;

    }
}
