namespace Movie.API.Responses.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public string? Avatar { get; set; }
        public string? AvatarUrl { get; set; } = default!;
        public string RoleName { get; set; } = default!;

    }
    public class UserAvatar
    {
        public string UserName { get; set; } = default!;
        public string? Avatar { get; set; } = default!;
        public string? AvatarUrl { get; set; } = default!;

    }
}
