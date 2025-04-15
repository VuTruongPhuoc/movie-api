using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Episodes
{
    public class GetEpisodesbyFilmQuery : IRequest<Response>
    {
        public int FilmId { get; set; } = default!;
    }
}
