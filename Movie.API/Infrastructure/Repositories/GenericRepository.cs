
using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Common;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T?> GetByIdAsync(object id);
        Task<T> AddAsync(T Entity);
        T UpdateAsync(T Entity);
        Task<bool> DeleteAsync(object id);
        Task SaveAsync();

    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            var result = await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await _dbSet.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList<T>(result, pageNumber, totalPages);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public T UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var result = false;
            var obj = await _dbSet.FindAsync(id);
            if (obj is not null)
            {
                _dbSet.Remove(obj);
                result = true;
            }
            return result;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
