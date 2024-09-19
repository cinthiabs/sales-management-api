using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct
    {
        Task<Response<Products>> CreateProductAsync(ProductsDto productDto);
        Task<Response<Products>> UpdateProductAsync(ProductsDto productDto, int id);
        Task<Response<bool>> DeleteProductAsync(int id);
        Task<Response<Products>> GetProductsAsync();
        Task<Response<Products>> GetByIdProductAsync(int id);
    }
}
