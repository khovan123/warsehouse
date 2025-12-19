using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class BusinessPartnerService : IBusinessPartnerService
    {
        private readonly IBusinessPartnerRepository _businessPartnetRepository;

        public BusinessPartnerService(IBusinessPartnerRepository businessPartnetRepository)
        {
            _businessPartnetRepository = businessPartnetRepository;
        }
        public async Task<ApiResponse<BusinessPartnerDTO.Response>> GetAll(CancellationToken ct)
        {
            var businessPartners = await _businessPartnetRepository.GetAll(ct);
            var data = new BusinessPartnerDTO.Response(businessPartners);

            return new ApiResponse<BusinessPartnerDTO.Response>(data);
        }
    }
}
