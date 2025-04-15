using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetReviewsResponse : Response
    {
        public List<ReviewDTO> Reviews { get; set; } = default!;
    }
    public class AddReviewResponse : Response
    {
        public ReviewDTO Review { get; set; } = default!;
    }
    public class UpdateReviewResponse : Response
    {
        public ReviewDTO Review { get; set; } = default!;
    }
    public class DeleteReviewResponse : Response
    {
        public ReviewDTO Review { get; set; } = default!;
    }
}
