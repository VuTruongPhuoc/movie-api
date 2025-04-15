using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Reviews
{
    public class DeleteReviewCommand : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
