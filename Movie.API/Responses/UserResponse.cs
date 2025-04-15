using Movie.API.Responses.DTOs;

namespace Movie.API.Responses
{
    public class GetUserResponse : Response
    {
        public UserDTO User { get; set; } = default!;
    }
    public class GetUsersResponse : Response
    {
        public List<UserDTO> Users { get; set; } = default!;
    }
    public class AddUserResponse : Response
    {
        public UserDTO User { get; set; } = default!;
    }
    public class UpdateUserResponse : Response
    {
        public UserDTO User { get; set; } = default!;
    }
    public class UserAvatarResponse : Response
    {
        public UserAvatar User { get; set; } = default!;
    }
    public class DeleteUserResponse : Response
    {
        public UserDTO User { get; set; } = default!;
    }
}
