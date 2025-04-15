using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ChangeRoleAsync(string userName, string roleName);
        Task<bool> DeleteUserAsync(string username);
        Task<User?> GetByNameAsync(string userName);
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MovieDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserRepository(MovieDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> ChangeRoleAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return false;
            var userRole = await _dbContext.UserRoles.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var currentRole = await _roleManager.FindByIdAsync(userRole?.RoleId ?? string.Empty);
            await _userManager.RemoveFromRoleAsync(user, currentRole?.Name?.ToString() ?? string.Empty);
            await _userManager.AddToRoleAsync(user, roleName);

            return true;
        }
        public async new Task<User> AddAsync(User entity)
        {
            await _userManager.CreateAsync(entity);
            return await Task.FromResult(entity);
        }
        public async new Task<User> UpdateAsync(User entity)
        {
            await _userManager.UpdateAsync(entity);
            return await Task.FromResult(entity);
        }
        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null) return false;
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<User?> GetByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }
    }
}
