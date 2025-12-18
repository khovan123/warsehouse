using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
  public class GoodTransactionSerivce : IGoodTransactionService
  {
    private readonly IGoodTransactionRepository _goodTransactionRepository;

    public GoodTransactionSerivce(IGoodTransactionRepository goodTransactionRepository)
    {
      _goodTransactionRepository = goodTransactionRepository;
    }
    public async Task<ApiResponse<GoodTransactionDTO.Response>> GetAll(CancellationToken ct)
    {
      var goodTransactions = await _goodTransactionRepository.GetAll(ct);
      var data = new GoodTransactionDTO.Response(goodTransactions);
      return new ApiResponse<GoodTransactionDTO.Response>.SuccessBuilder(data, "Get resources successfully!", StatusCodes.Status200OK);
    }
  }
}