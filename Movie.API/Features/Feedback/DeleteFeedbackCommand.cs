using MediatR;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Feedbacks
{
    public class DeleteFeedbackCommand : DeleteFeedbackRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
