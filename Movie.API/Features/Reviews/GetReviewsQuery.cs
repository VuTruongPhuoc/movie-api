using MediatR;
using Movie.API.Requests.Pagination;
using Movie.API.Responses;

namespace Movie.API.Features.Reviews
{
    public class GetReviewsQuery : IRequest<Response>
    {
        public int FilmId { get; set; } = default!;
    }
}
