using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct
    {
        Task<Response<Products>> CreateProductAsync(Products product);
        Task<Response<Products>> UpdateProductAsync(Products product);
        Task<Response<bool>> DeleteProductAsync(int id);
        Task<Response<Products>> GetProductsAsync();
        Task<Response<Products>> GetByIdProductAsync(int id);
    }
}
