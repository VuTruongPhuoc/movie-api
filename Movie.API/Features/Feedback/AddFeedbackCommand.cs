using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Feedbacks
{
    public class AddFeedbackCommand : AddFeedbackRequest, IRequest<AddFeedbackResponse>
    {
        public string UserId { get; set; } = default!;
    }
}
