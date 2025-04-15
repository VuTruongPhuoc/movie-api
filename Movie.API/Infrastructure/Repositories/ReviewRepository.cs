using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Common;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetAllAsync(int filmId);
        Task<Review?> GetByFilmAsync(int filmid, string userid);
    }
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly MovieDbContext _dbContext;
        private readonly DbSet<Review> _reviewSet;
        public ReviewRepository(MovieDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _reviewSet = _dbContext.Set<Review>();
        }
        public async Task<List<Review>> GetAllAsync(int filmId)
        {
            var reviews = await _reviewSet.Where(x => x.FilmId == filmId).ToListAsync();

            return reviews;
        }
        public async Task<Review?> GetByFilmAsync(int filmid, string userid)
        {
            var review = await _reviewSet.FirstOrDefaultAsync(x => x.FilmId == filmid & x.UserId == userid);
            return review;
        }
    }
}
