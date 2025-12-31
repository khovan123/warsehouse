using Contract.Interfaces;

namespace Application.Dtos
{
    public sealed record LoginDTO
    {
        public sealed record Request(string Username = default!, string Password = default!) : IFlagValidatableRequest;

        public sealed record Response(UserDTO UserDTO = default!, string Token = default!);

        public sealed record ResponseWithRefreshToken(UserDTO UserDTO = default!, string Token = default!, string RefreshToken = default!)
        {
            public Response Response { get; set; } = new Response(UserDTO, Token);
            public string RefreshToken { get; set; } = RefreshToken;
        }

        public sealed record UserDTO(string Id = default!, string Username = default!, string Email = default!);

    }
}
