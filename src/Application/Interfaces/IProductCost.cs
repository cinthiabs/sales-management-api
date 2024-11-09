using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductCost
    {
        Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCostDto productCostDto);
        Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id);
    }
}
