using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ISale
    {
        Task<Response<List<Sales>>> ReadExcel(Stream stream);
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(List<Sales> sale);
        Task<Sales> UpdateSale(Sales sale);
        Task<Sales> DeleteSale(int id);
        Task<IEnumerable<Sales>> GetSales();
        Task<Response<Sales>> GetByIdSale(int id);
    }
}
