using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Sections
{
    public class GetSectionQuery : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
