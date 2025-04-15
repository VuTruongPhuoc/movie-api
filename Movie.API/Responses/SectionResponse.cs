using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetSectionsResponse : Response
    {
        public List<SectionDTO> Sections { get; set; } = default!;
    }
    public class AddSectionResponse : Response
    {
        public SectionDTO Section { get; set; } = default!;
    }
    public class UpdateSectionResponse : Response
    {
        public SectionDTO Section { get; set; } = default!;
    }
    public class DeleteSectionResponse : Response
    {
        public SectionDTO Section { get; set; } = default!;
    }
}
