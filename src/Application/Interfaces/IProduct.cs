
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct
    {
        Task<Products> CreateProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task<Products> DeleteProduct(int id);
        Task<Products> GetProducts();
        Task<Products> GetByIdProduct(int id);
    }
}
