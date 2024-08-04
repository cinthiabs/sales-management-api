using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProduct
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<Products>> CreateProductAsync(Products product)
        {
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Response<bool>> DeleteProductAsync(int id)
        {
            var existProduct = await _productRepository.GetByIdProductAsync(id);
            if (existProduct.IsSuccess)
            {
                var deleteProduct = await _productRepository.DeleteProductAsync(id);
                return deleteProduct;
            }
            return Response<bool>.Failure(Status.noDatafound);
        }

        public async Task<Response<Products>> GetByIdProductAsync(int id)
        {
            return await _productRepository.GetByIdProductAsync(id);
        }

        public async Task<Response<Products>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Response<Products>> UpdateProductAsync(Products product)
        {
            var existProduct = await _productRepository.GetByIdProductAsync(product.Id);
            if (existProduct.IsSuccess)
            {
                var updated = await _productRepository.UpdateProductAsync(product);
                return updated;
            }
            return existProduct;
        }
    }
}
