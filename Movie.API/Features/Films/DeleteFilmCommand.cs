using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Films
{
    public class DeleteFilmCommand : DeleteFilmRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
