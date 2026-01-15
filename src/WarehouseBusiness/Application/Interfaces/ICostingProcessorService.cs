using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
  public interface ICostingProcessorService
  {
    Task ProcessBatchAsync(long fromSeqExclusive, int batchSize, CancellationToken ct);
    Task<CostingBatchResult> ProcessBatchWithResultAsync(long fromSeqExclusive, int batchSize, CancellationToken ct);
  }
}