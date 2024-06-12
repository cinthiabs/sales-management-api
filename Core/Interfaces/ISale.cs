using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISale
    {
        Task<string> ReadExcelExcelToJson(Stream stream);
        Task<Sales> CreateSale(Sales sale);
        Task<Sales> UpdateSale(Sales sale);
        Task<Sales> DeleteSale(int id);
        Task<Sales> GetSales();
        Task<Sales> GetByIdSale(int id);
    }
}
