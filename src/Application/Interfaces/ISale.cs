using Entities.Entities;

namespace Application.Interfaces
{
    public interface ISale
    {
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(List<Sales> sale);
        Task<bool> UpdateSale(Sales sale);
        Task<bool> DeleteSale(int id);
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetByIdSale(int id);
        Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd);
    }
}
