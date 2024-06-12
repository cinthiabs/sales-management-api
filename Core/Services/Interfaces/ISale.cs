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
        Task<List<Sales>> ReadExcel(Stream stream);
        Task<Sales> CreateSale(Sales sale);
        Task<string> CreateSaleList(List<Sales> sale);
        Task<Sales> UpdateSale(Sales sale);
        Task<Sales> DeleteSale(int id);
        Task<Sales> GetSales();
        Task<Sales> GetByIdSale(int id);
    }
}
