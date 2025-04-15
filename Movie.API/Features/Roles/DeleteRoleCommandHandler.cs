using MediatR;
using Microsoft.AspNetCore.Identity;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;

namespace Movie.API.Features.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleRepository _roleRepository;
        public DeleteRoleCommandHandler(RoleManager<Role> roleManager, IRoleRepository roleRepository)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }
        public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                return await Task.FromResult(new DeleteRoleResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Role không tồn tại",
                    RoleName = request.RoleName
                });
            }
            if (await _roleRepository.DeleteRoleAsync(role.Name ?? string.Empty) == true)
            {
                await _roleRepository.SaveAsync();
            }
            ;
            return await Task.FromResult(new DeleteRoleResponse
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Xóa role thành công",
                RoleName = request.RoleName
            });
        }
    }
}
