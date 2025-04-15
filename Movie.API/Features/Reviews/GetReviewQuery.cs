using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Reviews
{
    public class GetReviewQuery : IRequest<Response>
    {
        public int FilmId { get; set; } = default!;
        public string? UserId { get; set; } = default!;
    }
}
