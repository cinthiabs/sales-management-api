using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductCost
    {
        Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCostDto productCostDto);
        Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id);
        Task<Response<ProductTotalCosts>> GetAllProductCostAsync();
        Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCostDto productCost, int id);
    }
}
