using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<Response<Products>> CreateProductAsync(Products prod);
        Task<Response<bool>> CreateProductListAsync(Products prod);
        Task<Response<Products>> UpdateProductAsync(Products prod);
        Task<Response<bool>> DeleteProductAsync(int id);
        Task<Response<Products>> GetProductsAsync();
        Task<Response<Products>> GetByIdProductAsync(int id);
        Task<Products> GetByNameProductAsync(string name);
        Task<Products> GetByProductsParametersAsync(Products prod);
    }
}
