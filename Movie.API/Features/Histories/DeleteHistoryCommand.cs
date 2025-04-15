using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Histories
{
    public class DeleteHistoryCommand : IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
