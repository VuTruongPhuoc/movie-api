using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly MovieDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UpdateUserCommandHandler(MovieDbContext dbContext, IUserRepository userRepository, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                return await Task.FromResult(new UpdateUserResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy user cần cập nhật"
                });
            }
            var checkuserdisplayname = await _dbContext.Users.SingleOrDefaultAsync(x => x.DisplayName == request.DisplayName);
            var checkuseremail = await _userManager.FindByEmailAsync(request.Email);
            if (checkuserdisplayname?.DisplayName != user.DisplayName && checkuserdisplayname != null || checkuseremail?.Email != user.Email && checkuseremail != null)
            {
                return await Task.FromResult(new UpdateUserResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Tên hiển thị hoặc email đã tồn tại"
                });
            }

            CustomMapper.Mapper.Map<UpdateUserCommand, User>(request, user);
            _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            return await Task.FromResult(new UpdateUserResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật thông tin user thành công",
                User = CustomMapper.Mapper.Map<UserDTO>(user)
            });

        }
    }
}
