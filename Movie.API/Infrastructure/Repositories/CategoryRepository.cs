using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Infrastructure.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>?> GetAllAsync();
        Task<List<Category>?> GetByNameAsync(string name);
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly MovieDbContext _dbContext;
        private readonly DbSet<Category> _categorySet;
        public CategoryRepository(MovieDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _categorySet = _dbContext.Set<Category>();

        }

        public async Task<List<Category>?> GetAllAsync()
        {
            return await _categorySet.ToListAsync() ?? [];
        }

        public async Task<List<Category>?> GetByNameAsync(string name)
        {
            return await _categorySet.Where(x => x.Name == name).ToListAsync() ?? [];
        }
    }
}
