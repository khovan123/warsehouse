using Application.Dtos;
using Application.Helper;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities.Weak;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ParetoService : IParetoService
    {
        private readonly IParetoRepository _paretoRepository;

        public ParetoService(IParetoRepository paretoRepository)
        {
            _paretoRepository = paretoRepository;
        }

        public async Task<ApiResponse<ParetoDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var paretoReports = await _paretoRepository.GetAllAsync(ct);

            var classifications = GetClassificationsFromParetoReports(paretoReports);

            var data = new ParetoDTO.Response(paretoReports, classifications);

            return new ApiResponse<ParetoDTO.Response>(
                data,
                "Get resources successfully!",
                StatusCodes.Status200OK
            );
        }

        private static List<ParetoDTO.Classification> GetClassificationsFromParetoReports(
            List<ParetoDetails> paretoReports)
        {
            var totalCount = paretoReports.Count;
            var totalValue = paretoReports.Sum(p => p.Value);

            return paretoReports
                .Where(p => p.TagEnum.HasValue)
                .GroupBy(p => p.TagEnum!.Value)
                .Select(g =>
                {
                    var groupValue = g.Sum(p => p.Value);
                    var count = g.Count();

                    return new ParetoDTO.Classification(
                        Tag: g.Key.ToString(),
                        TagPercentage: Calculator.CalculatePercentage(count, totalCount),
                        ValuePercentage: Calculator.CalculatePercentage(groupValue, totalValue)
                    );
                })
                .OrderBy(c => c.Tag)
                .ToList();
        }
    }
}
