using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<Products> CreateProductAsync(Products prod);
        Task<bool> CreateProductListAsync(Products prod);
        Task<bool> UpdateProductAsync(Products prod);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Products>> GetProductsAsync();
        Task<Products> GetByIdProductAsync(int id);
        Task<Products> GetByProductsParametersAsync(Products prod);
    }
}
