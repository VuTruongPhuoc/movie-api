using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetFeedbacksResponse : Response
    {
        public List<CountryDTO> Feedbacks { get; set; } = default!;
    }
    public class AddFeedbackResponse : Response
    {
        public FeedbackDTO Feedback { get; set; } = default!;
    }
    public class UpdateFeedbackResponse : Response
    {
        public FeedbackDTO Feedback { get; set; } = default!;
    }
    public class DeleteFeedbackResponse : Response
    {
        public FeedbackDTO Feedback { get; set; } = default!;
    }
}
