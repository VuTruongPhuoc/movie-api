using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Tracks
{
    public class DeleteTrackCommand : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
