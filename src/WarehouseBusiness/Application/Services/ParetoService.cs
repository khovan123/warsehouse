using System.Linq;
using Application.DTOs;
using Application.Helper;
using Application.Interfaces;
using Contract.Responses;
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

        public async Task<ApiResponse<ParetoDTO.Response>> GetAll(CancellationToken ct)
        {
            var paretoReports = await _paretoRepository.GetAllWithDetails(ct);

            var totalCount = paretoReports.Count;
            var totalValue = paretoReports.Sum(p => p.Value);

            var classifications = BuildClassifications(paretoReports, totalCount, totalValue);

            var data = new ParetoDTO.Response(paretoReports, classifications);

            return new ApiResponse<ParetoDTO.Response>(
                data,
                "Get resources successfully!",
                StatusCodes.Status200OK
            );
        }

        private static List<ParetoDTO.Classification> BuildClassifications(
            List<Domain.Entities.Weak.ParetoReport> paretoReports,
            int totalCount,
            double totalValue)
        {
            return paretoReports
                .Where(p => p.TagEnum.HasValue)
                .GroupBy(p => p.TagEnum!.Value)
                .Select(g =>
                {
                    var groupValue = g.Sum(p => p.Value);
                    var count = g.Count();

                    return new ParetoDTO.Classification(
                        Tag: g.Key.ToString(),
                        TagPercentage: PercentageCalculator.Calculate(count, totalCount),
                        ValuePercentage: PercentageCalculator.Calculate(groupValue, totalValue)
                    );
                })
                .OrderBy(c => c.Tag)
                .ToList();
        }
    }
}
