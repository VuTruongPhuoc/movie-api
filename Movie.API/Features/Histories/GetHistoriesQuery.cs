using MediatR;
using Movie.API.Requests.Pagination;
using Movie.API.Responses;

namespace Movie.API.Features.Histories
{
    public class GetHistoriesQuery : IRequest<Response>
    {
        public string UserId { get; set; } = default!;
        public Pagination Pagination { get; set; } = default!;
    }
}
