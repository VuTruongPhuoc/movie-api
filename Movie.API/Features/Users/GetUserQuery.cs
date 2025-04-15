using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Users
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public string UserName { get; set; } = default!;
    }
}
