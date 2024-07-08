
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct
    {
        Task<Products> CreateProductAsync(Products product);
        Task<Products> UpdateProductAsync(Products product);
        Task<Products> DeleteProductAsync(int id);
        Task<Products> GetProductsAsync();
        Task<Products> GetByIdProductAsync(int id);
    }
}
