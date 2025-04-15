using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Films
{
    public class GetFilmQuery : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
