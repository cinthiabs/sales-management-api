using Dapper;
using Domain.Entities;
using Infrastructure.Connection;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class SaleRepository(IConfiguration configuration) : RepositoryBase(configuration), ISaleRepository
    {
        public async Task<Sales> CreateSaleAsync(Sales sale)
        {
            var parameters = new
            {
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.DateSale,
                @DateCreate = DateTime.Now
            };

            var Id = await _conn.ExecuteScalarAsync(SaleSqlQuery.QueryCreateSale, parameters);
            var created = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, new { Id });
            return created!;
        }

        public async Task<bool> CreateSaleListAsync(Sales sale)
        {
           if (string.IsNullOrEmpty(sale.Name))
           {
               return false;
           }

           var parameters = new
           {
               sale.Name,
               sale.Price,
               sale.Details,
               sale.Quantity,
               sale.DateSale,
               sale.Pay,
               DateCreate = DateTime.Now
           };
           int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);
           return result > 0;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var parameters = new { id };
            var  delete = await _conn.ExecuteAsync(SaleSqlQuery.QueryDeleteSale, parameters);
            return delete > 0;
        }

        public Task<IEnumerable<Sales>> GetByFiltersAsync(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<Sales> GetByIdSaleAsync(int id)
        {
            var parameters = new { id };
            var sale = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, parameters);
            return sale!;
        }

        public async Task<IEnumerable<Sales>> GetSalesAsync()
        {
            var sale = await _conn.QueryAsync<Sales>(SaleSqlQuery.QuerySelectSale);
            return sale;
        }

        public async Task<bool> UpdateSaleAsync(Sales sale)
        {
            var parameters = new 
            {
                sale.Id,
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.Pay,
                sale.DateSale,
                @DateEdit = DateTime.Now
                
            };

            var update = await _conn.ExecuteAsync(SaleSqlQuery.QueryUpdateSale, parameters);
            return update > 0;
        }

        public async Task<Sales> GetBySaleParametersAsync(Sales sale)
        {
            var parameters = new
            {
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.DateSale
            };
            var result = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryBySaleParameters, parameters);
            return result ?? new Sales { Name = "" };
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var parameters = new
            {
                dateIni,
                dateEnd
            };
            var rel = await _conn.QueryAsync<RelQuantity>(SaleSqlQuery.GetRelQuantity, parameters);
            return rel!;
        }
    }
}
