using Movie.API.Models.Domain.Common;
using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetFilmResponse : Response
    {
        public FilmDTO Film { get; set; } = default!;
    }
    public class GetFilmsResponse : Response
    {
        public List<FilmDTO> Films { get; set; } = default!;
    }
    public class AddFilmResponse : Response
    {
        public FilmDTO Film { get; set; } = default!;
    }
    public class UpdateFilmResponse : Response
    {
        public FilmDTO Film { get; set; } = default!;
    }
    public class DeleteFilmResponse : Response
    {
        public FilmDTO Film { get; set; } = default!;
    }
    public class FilmImageResponse : Response
    {
        public FilmImage Film { get; set; } = default!;
    }
    public class FilmPosterResponse : Response
    {
        public FilmPoster Film { get; set; } = default!;
    }
    public class GetFilmBySlugResponse : Response
    {
        public FilmDTO Film { get; set; } = default!;
        public List<EpisodeDTO> Episodes { get; set; } = default!;
    }

    public class FilterFilmResponse : Response
    {
        public string Name { get; set; } = default!;
        public PaginatedList<FilmFilter> Data { get; set; } = default!;
    }
}
