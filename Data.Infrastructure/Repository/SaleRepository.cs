using Core.Repositories;
using Dapper;
using Data.Infrastructure.Queries;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data.Infrastructure.Repository
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
        public async Task<Sales> CreateSale(Sales sale)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DateSale", sale.DateSale);
            parameters.Add("@Name", sale.Name);
            parameters.Add("@Details", sale.Details);
            parameters.Add("@Quantity", sale.Quantity);
            parameters.Add("@Price", sale.Price);
            parameters.Add("@DateCreate", DateTime.Now);

            int id = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);
            sale.Id = id;
            return sale;
        }

        public async Task<bool> CreateSaleList(Sales sale)
        {
            bool success = false;

            if (!string.IsNullOrEmpty(sale.Name))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DateSale", sale.DateSale);
                parameters.Add("@Name", sale.Name);
                parameters.Add("@Details", sale.Details);
                parameters.Add("@Quantity", sale.Quantity);
                parameters.Add("@Price", sale.Price);
                parameters.Add("@DateCreate", DateTime.Now);

                int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);

                if (result > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public async Task<bool> DeleteSale(int id)
        {
            var parameters = new { id };
            var  delete = await _conn.ExecuteAsync(SaleSqlQuery.QueryDeleteSale, parameters);
            return delete > 0;
            
        }

        public Task<IEnumerable<Sales>> GetByFilters(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<Sales> GetByIdSale(int id)
        {
            var parameters = new { id };
            var sale = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, parameters);
            return sale!;
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            var sale = await _conn.QueryAsync<Sales>(SaleSqlQuery.QuerySelectSale);
            return sale;
        }

        public async Task<bool> UpdateSale(Sales sale)
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

        public async Task<Sales> GetBySaleParameters(Sales sale)
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

        public async Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd)
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
