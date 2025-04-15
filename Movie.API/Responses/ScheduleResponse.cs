using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetSchedulesResponse : Response
    {
        public List<ScheduleDTO> Schedules { get; set; } = default!;
    }
    public class AddScheduleResponse : Response
    {
        public ScheduleDTO Schedule { get; set; } = default!;
    }
    public class UpdateScheduleResponse : Response
    {
        public ScheduleDTO Schedule { get; set; } = default!;
    }
    public class DeleteScheduleResponse : Response
    {
        public ScheduleDTO Schedule { get; set; } = default!;
    }
}
