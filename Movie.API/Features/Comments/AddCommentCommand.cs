using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Comments
{
    public class AddCommentCommand : AddCommentRequest, IRequest<AddCommentResponse>
    {
        public string UserId { get; set; } = default!;
    }
}
