using Core.Abstractions.Repositories;
using Dapper;
using Data.Infrastructure.Queries;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public Task<Sales> CreateSale(Sales sale)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateSaleList(List<Sales> sales)
        {
            bool success = false;

            foreach (var sale in sales)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DateSale", sale.DateSale);
                parameters.Add("@Name", sale.Name);
                parameters.Add("@Details", sale.Details);
                parameters.Add("@Quantity", sale.Quantity);
                parameters.Add("@Price", sale.Price);
                parameters.Add("@DataCreate", DateTime.Now);

                int result = await _conn.ExecuteAsync(SaleSqlQuery.QueryCreateSaleList, parameters);

                if (result > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public Task<Sales> DeleteSale(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Sales> GetByIdSale(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Sales> GetSales()
        {
            throw new NotImplementedException();
        }

        public Task<Sales> UpdateSale(Sales sale)
        {
            throw new NotImplementedException();
        }
    }
}
