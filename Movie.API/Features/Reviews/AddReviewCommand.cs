using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Reviews
{
    public class AddReviewCommand : AddReviewRequest, IRequest<Response>
    {
        public string UserId { get; set; } = default!;
    }
}
