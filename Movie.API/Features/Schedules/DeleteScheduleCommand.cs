using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Schedules
{
    public class DeleteScheduleCommand : DeleteScheduleRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
