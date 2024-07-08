using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISale
    {
        Task<Sales> CreateSaleAsync(Sales sale);
        Task<bool> CreateSaleListAsync(List<Sales> sale);
        Task<bool> UpdateSaleAsync(Sales sale);
        Task<bool> DeleteSaleAsync(int id);
        Task<IEnumerable<Sales>> GetSalesAsync();
        Task<Sales> GetByIdSaleAsync(int id);
        Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd);
    }
}
