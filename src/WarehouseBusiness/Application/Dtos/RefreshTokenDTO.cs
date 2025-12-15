namespace Application.Dtos
{
    public record RefreshTokenDTO
    {
        public record Response
        {
            public string AccessToken { get; set; } = default!;
        }

        public record ResponseWithRefreshToken: Response
        {
            public Response Response { get; set; } = default!;
            public string RefreshToken { get; set; } = default!;
        }
    }
}
