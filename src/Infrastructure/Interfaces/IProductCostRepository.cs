using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductCostRepository
    {
        Task<Response<ProductTotalCosts>> CreateProductTotalCostAsync(ProductTotalCosts productCost, int product);
        Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id);
        Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCosts productCost, int id);
    }
}
