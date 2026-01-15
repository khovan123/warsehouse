using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MongoDB.Bson;

namespace Application.Services
{
  public class MovementService : IMovementService
  {
    private readonly IMovementRepository _movementRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDocumentPostingService _posting;

    public MovementService(IMovementRepository movementRepository,
      IProductRepository productRepository,
      IDocumentPostingService posting)
    {
      _movementRepository = movementRepository;
      _productRepository = productRepository;
      _posting = posting;
    }

    public async Task<ApiResponse<MovementDTO.Response>?> GetAllAsync(CancellationToken ct)
    {
      var movements = await _movementRepository.GetAllAsync(ct);
      var data = new MovementDTO.Response(movements);
      return new ApiResponse<MovementDTO.Response>(data);
    }

    public async Task<ApiResponse<MovementDTO.ReportResponse>?> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct)
    {
      var movementReports = await _movementRepository.GetAllWithDetailsAsync(type, period, ct);
      var data = new MovementDTO.ReportResponse(movementReports);
      return new ApiResponse<MovementDTO.ReportResponse>(data);
    }

    public async Task<ApiResponse<MovementDTO.SummaryResponse>?> GetSummaryAsync(SummaryPeriod period, CancellationToken ct)
    {
      var movementSummaries = await _movementRepository.GetSummaryAsync(period, ct);
      var data = new MovementDTO.SummaryResponse(movementSummaries);
      return new ApiResponse<MovementDTO.SummaryResponse>(data);
    }

    public async Task<ApiResponse<MovementDTO.CreateResponse>?> CreateAsync(MovementDTO.CreateRequest request, CancellationToken ct)
    {
      var mv = new Movement
      {
        Id = ObjectId.GenerateNewId().ToString(),
        DocNo = request.DocNo,
        PostedAt = request.PostedAt,
        FromWarehouse = request.FromWarehouse,
        ToWarehouse = request.ToWarehouse,
        Status = FlowStatus.DRAFT,
        Reason = request.Reason,
        CreatedBy = request.CreatedBy,
        Lines = request.Lines.Select(l => new MovementLine
        {
          ProductId = l.ProductId,
          Qty = l.Qty,
          Uom = l.Uom,
          FromBin = l.FromBin,
          ToBin = l.ToBin
        }).ToList()
      };

      await _movementRepository.InsertAsync(mv, ct);
      return new ApiResponse<MovementDTO.CreateResponse>(new MovementDTO.CreateResponse(mv.Id));
    }

    public async Task<ApiResponse<MovementDTO.ChangeStatusResponse>?> CompleteAsync(string id, CancellationToken ct)
    {
      var mv = await _movementRepository.GetRawByIdAsync(id, ct);
      if (mv == null)
        return new ApiResponse<MovementDTO.ChangeStatusResponse>(new MovementDTO.ChangeStatusResponse(id, FlowStatus.DRAFT), "Movement not found", 404);

      mv.Status = FlowStatus.COMPLETED;
      await _movementRepository.UpdateStatusAsync(id, mv.Status, ct);

      return new ApiResponse<MovementDTO.ChangeStatusResponse>(new MovementDTO.ChangeStatusResponse(id, mv.Status));
    }

    public async Task<ApiResponse<MovementDTO.PostResponse>?> PostAsync(string id, CancellationToken ct)
    {
      var mv = await _movementRepository.GetRawByIdAsync(id, ct);
      if (mv == null)
        return new ApiResponse<MovementDTO.PostResponse>(new MovementDTO.PostResponse(id, "Movement not found."), "Movement not found", 404);

      await _posting.PostMovementAsync(mv, ct);

      await _movementRepository.UpdateStatusAsync(id, FlowStatus.POSTED, ct);

      return new ApiResponse<MovementDTO.PostResponse>(new MovementDTO.PostResponse(id, "Posted"));
    }
  }
}