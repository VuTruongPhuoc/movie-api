using MediatR;
using Microsoft.AspNetCore.Identity;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        private readonly MovieDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRepository _userRepository;
        public AddUserCommandHandler(MovieDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }
        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userManager.FindByNameAsync(request.UserName) != null)
                {
                    return await Task.FromResult(new AddUserResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "Người dùng đã tồn tại"
                    });
                }
                if (await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return await Task.FromResult(new AddUserResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "Email này đã được đăng ký"
                    });
                }
                var user = CustomMapper.Mapper.Map<AddUserCommand, User>(request);
                await _userRepository.AddAsync(user);
                await _userManager.AddPasswordAsync(user, request.UserName);
                await _userManager.AddToRoleAsync(user, "Customer");
                await _userRepository.SaveAsync();
                var dto = CustomMapper.Mapper.Map<UserDTO>(user);
                var role = _dbContext.UserRoles.FirstOrDefault(r => r.UserId == user.Id);
                if (role is not null)
                {
                    dto.RoleName = (await _roleManager.FindByIdAsync(role.RoleId))?.Name!;
                }
                return await Task.FromResult(new AddUserResponse()
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Đăng ký người dùng thành công",
                    User = dto
                });
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
