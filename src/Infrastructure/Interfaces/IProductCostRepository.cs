using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductCostRepository
    {
        Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCosts productCost);
        Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id);
        Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCosts productCost, int id);
    }
}
