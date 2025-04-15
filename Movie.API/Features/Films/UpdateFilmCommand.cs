using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Films
{
    public class UpdateFilmCommand : UpdateFilmRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
