using Movie.API.Models.Domain.Common;


namespace Movie.API.Models.Domain.Entities
{
    public class Server : BaseDomainEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public virtual List<EpisodeServer> EpisodeServers { get; set; } = [];
        public virtual List<Episode> Episodes { get; set; } = [];
    }
}
