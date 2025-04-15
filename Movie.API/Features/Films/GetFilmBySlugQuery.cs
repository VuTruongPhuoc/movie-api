using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Films
{
    public class GetFilmBySlugQuery : IRequest<GetFilmBySlugResponse>
    {
        public string Slug { get; set; } = default!;
    }
}
