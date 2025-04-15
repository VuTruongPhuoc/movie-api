using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Films
{
    public class GetFilmBySlugQueryHandler : IRequestHandler<GetFilmBySlugQuery, GetFilmBySlugResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly MovieDbContext _dbContext;

        public GetFilmBySlugQueryHandler(
            IFilmRepository filmRepository,
            ICountryRepository countryRepository,
            IScheduleRepository scheduleRepository,
            ISectionRepository sectionRepository,
            MovieDbContext dbContext)
        {
            _filmRepository = filmRepository;
            _countryRepository = countryRepository;
            _scheduleRepository = scheduleRepository;
            _sectionRepository = sectionRepository;
            _dbContext = dbContext;
        }

        public async Task<GetFilmBySlugResponse> Handle(GetFilmBySlugQuery request, CancellationToken cancellationToken)
        {
            if (request.Slug is null)
            {
                return new GetFilmBySlugResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = string.Empty
                };
            }
            var film = await _dbContext.Films
                .Include(f => f.FilmCategories)
                    .ThenInclude(fc => fc.Category)
                .Include(f => f.Schedule)
                .Include(f => f.Country)
                .Include(f => f.Reviews)
                .SingleOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);

            if (film is null)
            {
                return new GetFilmBySlugResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = string.Empty
                };
            }

            var filmDto = CustomMapper.Mapper.Map<FilmDTO>(film);
            filmDto.Categories = film.FilmCategories
                .Select(fc => CustomMapper.Mapper.Map<CategoryDTO>(fc.Category))
                .ToList();
            filmDto.Schedule = CustomMapper.Mapper.Map<ScheduleDTO>(film.Schedule);
            filmDto.Country = CustomMapper.Mapper.Map<CountryDTO>(film.Country);

            var reviews = film.Reviews;
            filmDto.Review = new ReviewTotal()
            {
                Count = reviews.Count,
                AvgRate = reviews.Count > 0 ? (double)reviews.Sum(x => x.Rate) / reviews.Count : 0
            };
            var episodes = await _dbContext.Episodes
                .Where(e => e.FilmId == film.Id)
                .ToListAsync(cancellationToken);
            var episodeDtos = new List<EpisodeDTO>();
            var sectionDict = _dbContext.Sections.Where(w => episodes.Select(s => s.SectionId).Contains(w.Id)).ToDictionary(k => k.Id, v => v.Name);
            foreach (var episode in episodes)
            {
                var episodeDto = CustomMapper.Mapper.Map<EpisodeDTO>(episode);
                episodeDto.FilmName = film.Name;
                episodeDto.SectionName = sectionDict.TryGetValue(episode.SectionId, out var name) ? name : string.Empty;
                episodeDtos.Add(episodeDto);
            }

            return await Task.FromResult(new GetFilmBySlugResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Thành công",
                Film = filmDto,
                Episodes = episodeDtos
            });
        }
    }
}
