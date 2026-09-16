using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;

namespace Application.Services
{
    public class BinService : IBinService
    {
        private readonly IBinRepository _binRepository;

        public BinService(IBinRepository binRepository)
        {
            _binRepository = binRepository;
        }

        public async Task<ApiResponse<BinDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var bins = await _binRepository.GetAllAsync(ct);
            var data = new BinDTO.Response(bins);
            return new ApiResponse<BinDTO.Response>(data);
        }
    }
}
