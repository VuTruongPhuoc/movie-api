using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Users
{
    public class UpdateUserCommand : UpdateUserRequest, IRequest<UpdateUserResponse>
    {
        public string UserName { get; set; } = default!;
    }
}
