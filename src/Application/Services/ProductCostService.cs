using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ProductCostService(IProductCostRepository productCostRepository, IMapper mapper) : IProductCost
    {
        private readonly IProductCostRepository _productCostRepository = productCostRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCostDto productCostDto)
        {
            var productTotalCosts = _mapper.Map<ProductTotalCosts>(productCostDto);
            productTotalCosts.ProductCost = _mapper.Map<List<ProductCost>>(productCostDto.UnitCost);
            return await _productCostRepository.CreateProductTotalCostAsync(productTotalCosts, productCostDto.IdProduct);
        }

        public async Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id)
        {
            return await _productCostRepository.GetProductCostByIdAsync(id);
        }

        public async Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCostDto productCostDto, int id)
        {
            var mapProductCost = _mapper.Map<ProductTotalCosts>(productCostDto);
            return await _productCostRepository.UpdateProductCostAsync(mapProductCost, id);
        }
    }
}
