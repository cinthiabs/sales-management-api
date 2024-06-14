using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ISaleRepository
    {
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(List<Sales> sale);
        Task<int> UpdateSale(Sales sale);
        Task<int> DeleteSale(int id);
        Task <IEnumerable<Sales>> GetSales();
        Task<Sales> GetByIdSale(int id);
    }
}
