namespace Application.UnitTests.Dtos
{
  public record LoginTestDTO
  {
    public record Request(string Username = default!, string Password = default!);

    public record Response(UserDTO UserDTO = default!, string Token = default!);

    public record ResponseWithRefreshToken(UserDTO UserDTO = default!, string Token = default!, string RefreshToken = default!)
    {
      public Response Response { get; set; } = new Response(UserDTO, Token);
      public string RefreshToken { get; set; } = RefreshToken;
    }

    public record UserDTO(string Id = default!, string Username = default!, string Email = default!);

  }
}
