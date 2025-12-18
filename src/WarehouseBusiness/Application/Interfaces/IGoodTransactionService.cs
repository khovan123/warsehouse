using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
  public interface IGoodTransactionService
  {
    Task<ApiResponse<GoodTransactionDTO.Response>> GetAll(CancellationToken ct);

  }
}