using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class LoginRespone : Response
    {
        public string AccessToken { get; set; } = default!;
        public DateTime Expiration { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public UserDTO User { get; set; } = default!;
    }
    public class RegisterResponse : Response { }
    public class RefreshTokenRespone : Response { }

}
