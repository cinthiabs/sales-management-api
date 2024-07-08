using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SqlConnection _conn;
        public SaleRepository(IConfiguration configuration)
        {
            _conn = new SqlConnection();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conn = new SqlConnection(connectionString);
        }
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

            int id = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);
            sale.Id = id;
            return sale;
        }

        public async Task<bool> CreateSaleListAsync(Sales sale)
        {
            bool success = false;

            if (!string.IsNullOrEmpty(sale.Name))
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
                int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);

                if (result > 0)
                {
                    success = true;
                }
            }

            return success;
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
