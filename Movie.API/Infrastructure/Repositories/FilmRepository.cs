using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Common;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IFilmRepository : IGenericRepository<Film>
    {
        Task<List<Film>> GetAllAsync();
        Task<Film?> GetBySlugAsync(string slug);

        Task<PaginatedList<Film>> GetByTypeAsync(int pagenumber, int pagesize, int type);
        Task<PaginatedList<Film>> GetByCategoryAsync(int pagenumber, int pagesize, int category);
        Task<PaginatedList<Film>> GetByNameAsync(int pagenumber, int pagesize, string name);
        Task<PaginatedList<Film>> Filter(int pagenumber, int pagesize, int? year, int? country, int? category);
    }
    public class FilmRepository : GenericRepository<Film>, IFilmRepository
    {
        private readonly MovieDbContext _dbContext;
        private readonly DbSet<Film> _filmSet;

        public FilmRepository(MovieDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _filmSet = _dbContext.Set<Film>();

        }

        public async Task<List<Film>> GetAllAsync()
        {
            return await _filmSet.ToListAsync();
        }

        public async Task<Film?> GetBySlugAsync(string slug)
        {
            return await _filmSet.SingleOrDefaultAsync(x => x.Slug == slug);
        }
        public async Task<PaginatedList<Film>> GetByTypeAsync(int pageNumber, int pageSize, int type)
        {
            var films = await _filmSet
                .Where(x => x.Type == type)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            var count = films.Count();
            var result = films.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList<Film>(result, pageNumber, totalPages);
        }
        public async Task<PaginatedList<Film>> GetByCategoryAsync(int pageNumber, int pageSize, int category)
        {
            var films = await _filmSet
                .Include(x => x.FilmCategories)
                    .ThenInclude(x => x.Category)
                .Where(x => x.FilmCategories.Any(fc => fc.CategoryId == category))
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            var count = films.Count();
            var result = films.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList<Film>(result, pageNumber, totalPages);
        }

        public async Task<PaginatedList<Film>> GetByNameAsync(int pageNumber, int pageSize, string name)
        {
            var films = await _filmSet
                .Where(x => !string.IsNullOrEmpty(name) &&
                             (x.Name.ToLower().Contains(name.ToLower()) || x.OriginName.ToLower().Contains(name.ToLower())))

                .ToListAsync();
            var count = films.Count();
            var result = films.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList<Film>(result, pageNumber, totalPages);
        }

        public async Task<PaginatedList<Film>> Filter(int pageNumber, int pageSize, int? year, int? country, int? category)
        {
            var query = _filmSet.Include(x => x.FilmCategories)
                        .ThenInclude(x => x.Category)
                        .AsQueryable();

            if (year != null)
            {
                query = query.Where(x => x.Year == year);
            }

            if (country != null)
            {
                query = query.Where(x => x.CountryId == country);
            }

            if (category != null)
            {
                query = query.Where(x => x.FilmCategories.Any(fc => fc.CategoryId == category));
            }

            var result = await query.Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            var count = query.Count();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<Film>(result, pageNumber, totalPages);
        }


    }
}
