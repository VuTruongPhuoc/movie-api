using MediatR;
using Movie.API.Requests.Pagination;
using Movie.API.Responses;

namespace Movie.API.Features.Users
{
    public class GetUsersQuery : IRequest<DataRespone>
    {
        public Pagination Pagination { get; set; } = default!;
    }
}
