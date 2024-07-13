using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProduct
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Products> CreateProductAsync(Products product)
        {

            var result = await _productRepository.CreateProductAsync(product);
            return result;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var record = await _productRepository.GetByIdProductAsync(id);
            if (record is not null)
            {
                var rowsAffected = await _productRepository.DeleteProductAsync(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Products> GetByIdProductAsync(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id);
            return product;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return products;
        }

        public async Task<bool> UpdateProductAsync(Products product)
        {
            var record = await _productRepository.GetByIdProductAsync(product.Id);
            if (record is not null)
            {
                var updated = await _productRepository.UpdateProductAsync(product);
                return updated;
            }
            return false;
        }
    }
}
