using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetHistoriesResponse : Response
    {
        public List<HistoryDTO> Histories { get; set; } = default!;
    }
    public class AddHistoryResponse : Response
    {
        public HistoryDTO History { get; set; } = default!;
    }
    public class DeleteHistoryResponse : Response
    {
        public HistoryDTO History { get; set; } = default!;
    }
}
