using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MongoDB.Bson;

namespace Application.Services
{
  public class GoodTransactionService : IGoodTransactionService
  {
    private readonly IGoodTransactionRepository _goodTransactionRepository;
    private readonly IDocumentPostingService _posting;

    public GoodTransactionService(IGoodTransactionRepository goodTransactionRepository, IDocumentPostingService posting)
    {
      _goodTransactionRepository = goodTransactionRepository;
      _posting = posting;
    }
    public async Task<ApiResponse<GoodTransactionDTO.Response>?> GetAllAsync(CancellationToken ct)
    {
      var goodTransactions = await _goodTransactionRepository.GetAllAsync(ct);
      var data = new GoodTransactionDTO.Response(goodTransactions);
      return new ApiResponse<GoodTransactionDTO.Response>(data);
    }
    public async Task<ApiResponse<GoodTransactionDTO.CreateResponse>?> CreateAsync(GoodTransactionDTO.CreateRequest request, CancellationToken ct)
    {
      var gt = new GoodTransaction
      {
        Id = ObjectId.GenerateNewId().ToString(),
        DocNo = request.DocNo,
        WarehouseId = request.WarehouseId,
        PostedAt = request.PostedAt,
        Description = request.Description,
        Status = FlowStatus.DRAFT,
        CreatedBy = request.CreatedBy,
        Lines = request.Lines.Select(l => new GoodTransactionLine
        {
          ProductId = l.ProductId,
          ExpectedQty = l.ExpectedQty,
          CountedQty = l.CountedQty,
          Difference = l.Difference,
          BinId = l.BinId,
          Uom = l.Uom
        }).ToList()
      };

      await _goodTransactionRepository.InsertAsync(gt, ct);
      return new ApiResponse<GoodTransactionDTO.CreateResponse>(new GoodTransactionDTO.CreateResponse(gt.Id));
    }

    public async Task<ApiResponse<GoodTransactionDTO.ChangeStatusResponse>?> CompleteAsync(string id, CancellationToken ct)
    {
      var gt = await _goodTransactionRepository.GetRawByIdAsync(id, ct);
      if (gt == null)
        return new ApiResponse<GoodTransactionDTO.ChangeStatusResponse>(new GoodTransactionDTO.ChangeStatusResponse(id, FlowStatus.DRAFT), "GoodTransaction not found", 404);

      gt.Status = FlowStatus.COMPLETED;
      await _goodTransactionRepository.UpdateStatusAsync(id, gt.Status, ct);

      return new ApiResponse<GoodTransactionDTO.ChangeStatusResponse>(new GoodTransactionDTO.ChangeStatusResponse(id, gt.Status));
    }

    public async Task<ApiResponse<GoodTransactionDTO.PostResponse>?> PostAsync(string id, CancellationToken ct)
    {
      var gt = await _goodTransactionRepository.GetRawByIdAsync(id, ct);
      if (gt == null)
        return new ApiResponse<GoodTransactionDTO.PostResponse>(new GoodTransactionDTO.PostResponse(id, "GoodTransaction not found."), "GoodTransaction not found", 404);

      if (gt.Status == FlowStatus.DRAFT)
        throw new InvalidOperationException("GoodTransaction must be Completed before posting.");

      await _posting.PostGoodTransactionAsync(gt, ct);
      await _goodTransactionRepository.UpdateStatusAsync(id, FlowStatus.POSTED, ct);

      return new ApiResponse<GoodTransactionDTO.PostResponse>(new GoodTransactionDTO.PostResponse(id, "Posted"));
    }
  }
}