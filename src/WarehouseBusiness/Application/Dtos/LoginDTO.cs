namespace Application.DTOs
{
    public class LoginDTO
    {
        public class Request
        {
            public string Username { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class Response(UserDTO userDTO = default!, string token = default!)
        {
            public UserDTO User { get; set; } = userDTO;
            public string Token { get; set; } = token;
        }

        public class UserDTO(string id = default!, string username = default!, string email = default!)
        {
            public string Id { get; set; } = id;
            public string Username { get; set; } = username;
            public string Email { get; set; } = email;
        }

    }
}
