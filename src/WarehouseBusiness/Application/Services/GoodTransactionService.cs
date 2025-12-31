using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;

namespace Application.Services
{
  public class GoodTransactionService : IGoodTransactionService
  {
    private readonly IGoodTransactionRepository _goodTransactionRepository;

    public GoodTransactionService(IGoodTransactionRepository goodTransactionRepository)
    {
      _goodTransactionRepository = goodTransactionRepository;
    }
    public async Task<ApiResponse<GoodTransactionDTO.Response>?> GetAllAsync(CancellationToken ct)
    {
      var goodTransactions = await _goodTransactionRepository.GetAllAsync(ct);
      var data = new GoodTransactionDTO.Response(goodTransactions);
      return new ApiResponse<GoodTransactionDTO.Response>(data);
    }
  }
}