using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Categories
{
    public class UpdateCategoryCommand : UpdateCategoryRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
