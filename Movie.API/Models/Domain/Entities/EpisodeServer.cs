namespace Movie.API.Models.Domain.Entities
{
    public class EpisodeServer
    {
        public int EpisodeId { get; set; } = default!;
        public int ServerId { get; set; } = default!;
        public virtual Episode Episode { get; set; } = default!;
        public virtual Server Server { get; set; } = default!;
    }
}
