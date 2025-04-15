using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Reviews
{
    public class UpdateReviewCommand : UpdateReviewRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
