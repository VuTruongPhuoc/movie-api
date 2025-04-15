using MediatR;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Features.Countries
{
    public class UpdateCountryCommand : UpdateCountryRequest, IRequest<Response>
    {
        public int Id { get; set; } = default!;
    }
}
