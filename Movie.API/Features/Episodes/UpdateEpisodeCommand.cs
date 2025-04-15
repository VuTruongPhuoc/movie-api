using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Episodes
{
    public class UpdateEpisodeCommand : UpdateEpisodeRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
        public new int FilmId { get; set; } = default!;
    }
}
