using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Movie.API.Models.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.API.Models.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? DisplayName { get; set; } = default!;
        public string? Avatar { get; set; } = default!;
        public string? AvatarUrl { get; set; } = default!;
        public string? RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiry { get; set; } = default!;
        public virtual ICollection<Review> Reviews { get; set; } = default!;
        public virtual ICollection<Comment> Comments { get; set; } = default!;
        public virtual ICollection<Feedback> Feedbacks { get; set; } = default!;
        public virtual ICollection<Track> Tracks { get; set; } = default!;
        public virtual ICollection<History> Histories { get; set; } = default!;
    }
}
