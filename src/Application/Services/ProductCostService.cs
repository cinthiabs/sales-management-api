using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public class ProductCostService : IProductCost
    {
        public Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCostDto productCostDto)
        {
            throw new NotImplementedException();
        }
    }
}
