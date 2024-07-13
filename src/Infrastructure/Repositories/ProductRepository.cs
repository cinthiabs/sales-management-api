using Dapper;
using Domain.Entities;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class ProductRepository(IConfiguration configuration) : RepositoryBase(configuration), IProductRepository
    {
        public async Task<Products> CreateProductAsync(Products prod)
        {
            var parameters = new
            {
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(ProductSqlQuery.QueryCreateProduct, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByIdProduct, new { Id });
            return created!;
        }

        public async Task<bool> CreateProductListAsync(Products prod)
        {
            if (string.IsNullOrEmpty(prod.Name))
            {
                return false;
            }
            var parameters = new
            {
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateCreate = DateTime.Now
            };
            int result = await _conn.ExecuteAsync(ProductSqlQuery.QueryCreateProduct, parameters);
            return result > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var parameters = new { id };
            var delete = await _conn.ExecuteAsync(ProductSqlQuery.QueryDeleteProduct, parameters);
            return delete > 0;
        }

        public async Task<Products> GetByIdProductAsync(int id)
        {
            var parameters = new { id };
            var product = await _conn.QueryFirstOrDefaultAsync<Products>(ProductSqlQuery.QueryGetByIdProduct, parameters);
            return product!;
        }

        public Task<Products> GetByProductsParametersAsync(Products prod)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            var products = await _conn.QueryAsync<Products>(ProductSqlQuery.QuerySelectProduct);
            return products;
        }

        public async Task<bool> UpdateProductAsync(Products prod)
        {
            var parameters = new
            {
                prod.Name,
                prod.Details,
                prod.Active,
                prod.Price,
                DateCreate = DateTime.Now
            };

            var update = await _conn.ExecuteAsync(ProductSqlQuery.QueryUpdateProduct, parameters);
            return update > 0;
        }
    }
}
