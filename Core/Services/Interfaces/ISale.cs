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
        Task<bool> ReadExcel(Stream stream);
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(List<Sales> sale);
        Task<bool> UpdateSale(Sales sale);
        Task<bool> DeleteSale(int id);
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetByIdSale(int id);
        Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd);
    }
}
