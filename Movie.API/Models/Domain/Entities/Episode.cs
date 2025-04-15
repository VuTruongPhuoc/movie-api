using Movie.API.Models.Domain.Common;


namespace Movie.API.Models.Domain.Entities
{
    public class Episode : BaseDomainEntity
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public int FilmId { get; set; } = default!;
        public int SectionId { get; set; } = default!;
        public long View { get; set; } = default!;
        public string Link { get; set; } = default!;
        public Film Film { get; set; } = default!;
        public virtual List<Server> Servers { get; set; } = [];
        public virtual List<EpisodeServer> EpisodeServers { get; set; } = [];
    }
}
