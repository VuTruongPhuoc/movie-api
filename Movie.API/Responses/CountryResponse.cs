using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetCountriesResponse : Response
    {
        public List<CountryDTO> Countries { get; set; } = default!;
    }
    public class AddCountryResponse : Response
    {
        public CountryDTO Country { get; set; } = default!;
    }
    public class UpdateCountryResponse : Response
    {
        public CountryDTO Country { get; set; } = default!;
    }
    public class DeleteCountryResponse : Response
    {
        public CountryDTO Country { get; set; } = default!;
    }
}
