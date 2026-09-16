using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
  public interface IGoodTransactionService
  {
    Task<ApiResponse<GoodTransactionDTO.Response>?> GetAllAsync(CancellationToken ct);
    Task<ApiResponse<GoodTransactionDTO.CreateResponse>?> CreateAsync(GoodTransactionDTO.CreateRequest request, CancellationToken ct);
    Task<ApiResponse<GoodTransactionDTO.ChangeStatusResponse>?> CompleteAsync(string id, CancellationToken ct);
    Task<ApiResponse<GoodTransactionDTO.PostResponse>?> PostAsync(string id, CancellationToken ct);
  }
}