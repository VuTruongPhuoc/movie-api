using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses.DTOs;

namespace Movie.API.Infrastructure.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<bool> DeleteRoleAsync(string rolename);
    }

    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly MovieDbContext _dbContext;
        public readonly RoleManager<Role> _roleManager;
        public RoleRepository(MovieDbContext context, RoleManager<Role> roleManager) : base(context)
        {
            _dbContext = context;
            _roleManager = roleManager;
        }
        public async new Task<Role> AddAsync(Role entity)
        {
            await _roleManager.CreateAsync(entity);
            return entity;
        }
        public async new Task<Role> UpdateAsync(Role entity)
        {
            await _roleManager.UpdateAsync(entity);
            return entity;
        }
        public async Task<bool> DeleteRoleAsync(string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            if (role is null) return false;
            await _roleManager.DeleteAsync(role);
            return true;
        }
    }
}
