using MediatR;
using Movie.API.Responses;

namespace Movie.API.Features.Categories
{
    public class GetCategoriesQuery : GetCategoriesResponse, IRequest<DataRespone>
    {
    }
}
