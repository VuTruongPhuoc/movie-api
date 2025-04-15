using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Histories
{
    public class AddHistoryCommand : AddHistoryRequest, IRequest<Response>
    {
        public string UserId { get; set; } = default!;
    }
}
