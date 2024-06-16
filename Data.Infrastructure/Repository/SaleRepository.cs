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
            parameters.Add("@DataCreate", DateTime.Now);

            int id = await _conn.QuerySingleAsync<int>(SaleSqlQuery.QueryCreateSale, parameters);
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
                parameters.Add("@DataCreate", DateTime.Now);

                int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSale, parameters);

                if (result > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public async Task<int> DeleteSale(int id)
        {
            var parameters = new { id };
            return  await _conn.ExecuteAsync(SaleSqlQuery.QueryGetByIdSale, parameters);
            
        }

        public Task<List<Sales>> GetByFilters(DateTime dataStart, DateTime dataEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<Sales> GetByIdSale(int id)
        {
            var parameters = new { id };
            var sale = await _conn.QueryFirstOrDefaultAsync<Sales>(SaleSqlQuery.QueryGetByIdSale, parameters);
            return sale!;
        }

        public Task<bool> GetBySaleParameters(Sales sale)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            return await _conn.QueryAsync<Sales>(SaleSqlQuery.QuerySelectSale);
        }

        public async Task<int> UpdateSale(Sales sale)
        {
            var parameters = new 
            {
                sale.Id,
                sale.Name,
                sale.Price,
                sale.Details,
                sale.Quantity,
                sale.Pay,
                @DataEdit = DateTime.Now
            };

            return await _conn.ExecuteAsync(SaleSqlQuery.QueryUpdateSale, parameters);
        }
    }
}
