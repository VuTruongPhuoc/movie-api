using System.ComponentModel.DataAnnotations;

namespace Movie.API.Requests
{

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
    public class RegisterRequest
    {
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;

    }
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
