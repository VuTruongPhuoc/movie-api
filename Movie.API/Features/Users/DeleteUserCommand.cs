using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Users
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public string UserName { get; set; } = default!;
    }
}
