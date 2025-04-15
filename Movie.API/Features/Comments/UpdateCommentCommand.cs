using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Comments
{
    public class UpdateCommentCommand : UpdateCommentRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
