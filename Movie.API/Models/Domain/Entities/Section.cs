using Movie.API.Models.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.API.Models.Domain.Entities
{
    public class Section : BaseDomainEntity
    {
        public string Name { get; set; } = default!;
        public virtual ICollection<Episode> Episodes { get; set; } = default!;
    }
}
