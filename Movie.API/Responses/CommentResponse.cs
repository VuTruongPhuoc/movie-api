using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetCommentsResponse : Response
    {
        public List<CommentDTO> Comments { get; set; } = default!;
    }
    public class AddCommentResponse : Response
    {
        public CommentDTO Comment { get; set; } = default!;
    }
    public class UpdateCommentResponse : Response
    {
        public CommentDTO Comment { get; set; } = default!;
    }
    public class DeleteCommentResponse : Response
    {
        public CommentDTO Comment { get; set; } = default!;
    }
}
