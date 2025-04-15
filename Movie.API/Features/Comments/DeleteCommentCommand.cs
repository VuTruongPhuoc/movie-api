using MediatR;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Comments
{
    public class DeleteCommentCommand : DeleteCommentRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
