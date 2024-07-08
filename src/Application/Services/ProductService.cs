using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProduct
    {
        public Task<Products> CreateProductAsync(Products product)
        {
            throw new NotImplementedException();
        }

        public Task<Products> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetByIdProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Products> UpdateProductAsync(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
