using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Application.Helper.Options;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Application.Workers
{
  public class CostingBackgroundService : BackgroundService
  {
    private readonly ILogger<CostingBackgroundService> _log;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptions<CostingWorkerOptions> _opt;

    public CostingBackgroundService(
      ILogger<CostingBackgroundService> log,
      IServiceScopeFactory scopeFactory,
      IOptions<CostingWorkerOptions> opt)
    {
      _log = log;
      _scopeFactory = scopeFactory;
      _opt = opt;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var options = _opt.Value;

      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          using var scope = _scopeFactory.CreateScope();

          var checkpointRepo = scope.ServiceProvider.GetRequiredService<ICostingCheckpointRepository>();
          var processor = scope.ServiceProvider.GetRequiredService<ICostingProcessorService>();

          var cp = await checkpointRepo.GetOrCreateAsync(stoppingToken);
          var fromSeq = cp.LastProcessedSeq;

          var result = await processor.ProcessBatchWithResultAsync(fromSeq, options.BatchSize, stoppingToken);

          if (result.ProcessedCount == 0)
          {
            await Task.Delay(options.PollDelayMs, stoppingToken);
            continue;
          }

          await PersistCheckpointWithRetryAsync(checkpointRepo, result.MaxSeqProcessed, cp.Version, stoppingToken);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
          break;
        }
        catch (Exception ex)
        {
          _log.LogError(ex, "Costing worker failed.");
          await Task.Delay(_opt.Value.ErrorDelayMs, stoppingToken);
        }
      }
    }

    private static async Task PersistCheckpointWithRetryAsync(
      ICostingCheckpointRepository checkpointRepo,
      long newSeq,
      long expectedVersion,
      CancellationToken ct)
    {
      for (int i = 1; i <= 3; i++)
      {
        try
        {
          await checkpointRepo.UpdateOptimisticAsync(newSeq, expectedVersion, ct);
          return;
        }
        catch (InvalidOperationException) when (i < 3)
        {
          var cp = await checkpointRepo.GetOrCreateAsync(ct);
          expectedVersion = cp.Version;
        }
      }

      await checkpointRepo.UpdateOptimisticAsync(newSeq, expectedVersion, ct);
    }
  }
}
