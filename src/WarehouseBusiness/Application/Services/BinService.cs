using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class BinService : IBinService
    {
        private readonly IBinRepository _binRepository;

        public BinService(IBinRepository binRepository)
        {
            _binRepository = binRepository;
        }

        public async Task<ApiResponse<BinDTO.Response>> GetAll(CancellationToken ct)
        {
            var bins = await _binRepository.GetAll(ct);
            var data = new BinDTO.Response(bins);
            return new ApiResponse<BinDTO.Response>.SuccessBuilder(data, null, StatusCodes.Status200OK);
        }
    }
}
