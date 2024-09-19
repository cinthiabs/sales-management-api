using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProduct
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<Products>> CreateProductAsync(ProductsDto productDto)
        {
            var mapProduct = _mapper.Map<Products>(productDto);
            return await _productRepository.CreateProductAsync(mapProduct);
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

        public async Task<Response<Products>> UpdateProductAsync(ProductsDto productDto, int Id)
        {
            var mapProduct = _mapper.Map<Products>(productDto);
            mapProduct.Id = Id;

            var existProduct = await _productRepository.GetByIdProductAsync(mapProduct.Id);
            if (existProduct.IsSuccess)
            {
                var updated = await _productRepository.UpdateProductAsync(mapProduct);
                return updated;
            }
            return existProduct;
        }
    }
}
