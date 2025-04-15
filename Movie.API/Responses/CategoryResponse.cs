using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetCategoriesResponse : Response
    {
        public List<CategoryDTO> Categories { get; set; } = default!;
    }
    public class AddCategoryResponse : Response
    {
        public CategoryDTO Category { get; set; } = default!;
    }
    public class UpdateCategoryResponse : Response
    {
        public CategoryDTO Category { get; set; } = default!;
    }
    public class DeleteCategoryResponse : Response
    {
        public CategoryDTO Category { get; set; } = default!;
    }
}
