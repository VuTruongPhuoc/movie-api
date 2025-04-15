using Microsoft.AspNetCore.Identity;

namespace Movie.API.Models.Domain.Entities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<RolePrivileges> RolePrivileges { get; set; } = default!;
    }
}
