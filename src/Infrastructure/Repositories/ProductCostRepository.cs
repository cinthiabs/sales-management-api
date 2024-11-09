using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class ProductCostRepository : IProductCostRepository
    {
        public Task<Response<ProductTotalCosts>> CreateProductCostAsync(ProductTotalCosts productCost)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProductTotalCosts>> GetProductCostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProductTotalCosts>> UpdateProductCostAsync(ProductTotalCosts productCost, int id)
        {
            throw new NotImplementedException();
        }
    }
}
