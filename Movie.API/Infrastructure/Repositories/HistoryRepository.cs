using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Common;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        Task<PaginatedList<History>> GetAllAsync(int pageNumber, int pageSize, string userId);
    }
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        private readonly MovieDbContext _dbContext;
        private readonly DbSet<History> _historySet;
        public HistoryRepository(MovieDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _historySet = _dbContext.Set<History>();
        }
        public async Task<PaginatedList<History>> GetAllAsync(int pageNumber, int pageSize, string userId)
        {
            var histories = await _historySet.Where(x => x.UserId == userId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = histories.Count();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<History>(histories, pageNumber, totalPages);
        }
    }
}
