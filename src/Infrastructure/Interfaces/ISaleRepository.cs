using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sales> CreateSaleAsync(Sales sale);
        Task<bool> CreateSaleListAsync(Sales sale);
        Task<bool> UpdateSaleAsync(Sales sale);
        Task<bool> DeleteSaleAsync(int id);
        Task <IEnumerable<Sales>> GetSalesAsync();
        Task<Sales> GetByIdSaleAsync(int id);
        Task<Sales> GetBySaleParametersAsync(Sales sale);
        Task<IEnumerable<Sales>> GetByFiltersAsync(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd);
    }
}
