using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetEpisodesResponse : Response
    {
        public List<EpisodeDTO> Episodes { get; set; } = default!;
    }
    public class AddEpisodeResponse : Response
    {
        public EpisodeDTO Episode { get; set; } = default!;
    }
    public class UpdateEpisodeResponse : Response
    {
        public EpisodeDTO Episode { get; set; } = default!;
    }
    public class DeleteEpisodeResponse : Response
    {
        public EpisodeDTO Episode { get; set; } = default!;
    }
}
