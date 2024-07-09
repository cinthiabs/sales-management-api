using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct
    {
        Task<Products> CreateProductAsync(Products product);
        Task<bool> UpdateProductAsync(Products product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Products>> GetProductsAsync();
        Task<Products> GetByIdProductAsync(int id);
    }
}
