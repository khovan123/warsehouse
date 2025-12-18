using Contract.Interfaces;

namespace Application.Dtos
{
    public class LoginDTO
    {
        public class Request : IFlagValidatableRequest
        {
            public string Username { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class Response(UserDTO userDTO = default!, string token = default!)
        {
            public UserDTO User { get; set; } = userDTO;
            public string Token { get; set; } = token;
        }

        public class ResponseWithRefreshToken(UserDTO userDTO, string token, string refreshToken) : Response
        {
            public Response Response { get; set; } = new Response(userDTO, token);
            public string RefreshToken { get; set; } = refreshToken;
        }

        public class UserDTO(string id = default!, string username = default!, string email = default!)
        {
            public string Id { get; set; } = id;
            public string Username { get; set; } = username;
            public string Email { get; set; } = email;
        }

    }
}
