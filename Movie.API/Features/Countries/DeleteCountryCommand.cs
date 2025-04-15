using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Countries
{
    public class DeleteCountryCommand : DeleteCountryRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
