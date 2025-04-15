using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.API.Requests
{
    public class ChangeRoleRequest
    {
        public string UserName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
    public class AddUserRequest
    {
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
    }
    public class UpdateUserRequest
    {
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }
    public class ChangeImageRequest
    {
        public string UserName { get; set; } = default!;
        [NotMapped]
        public IFormFile AvatarFile { get; set; } = default!;
    }
    public class DeleteUserRequest
    {
    }
}
