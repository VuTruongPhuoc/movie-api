using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Episodes
{
    public class DeleteEpisodeCommand : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
