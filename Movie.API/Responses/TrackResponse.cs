using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetTracksResponse : Response
    {
        public List<TrackDTO> Tracks { get; set; } = default!;
    }
    public class AddTrackResponse : Response
    {
        public TrackDTO Track { get; set; } = default!;
    }
    public class UpdateTrackResponse : Response
    {
        public TrackDTO Track { get; set; } = default!;
    }
    public class DeleteTrackResponse : Response
    {
        public TrackDTO Track { get; set; } = default!;
    }
}
