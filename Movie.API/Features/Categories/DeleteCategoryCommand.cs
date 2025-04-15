using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Categories
{
    public class DeleteCategoryCommand : DeleteCategoryRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
