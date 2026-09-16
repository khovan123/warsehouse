namespace Application.Dtos
{
    public sealed record RefreshTokenDTO
    {
        public sealed record Response(string AccessToken = default!);

        public sealed record ResponseWithRefreshToken(string AccessToken = default!, string RefreshToken = default!)
        {
            public Response Response { get; set; } = new Response(AccessToken);
            public string RefreshToken { get; set; } = RefreshToken;
        }
    }
}
