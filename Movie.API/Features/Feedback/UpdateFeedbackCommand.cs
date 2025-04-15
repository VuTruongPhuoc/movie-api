using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Feedbacks
{
    public class UpdateFeedbackCommand : UpdateCommentRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
