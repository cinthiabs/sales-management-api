using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ISaleRepository
    {
        Task<Response<Sales>> CreateSaleAsync(Sales sale);
        Task<Response<bool>> CreateSaleListAsync(Sales sale);
        Task<Response<Sales>> UpdateSaleAsync(Sales sale);
        Task<Response<bool>> DeleteSaleAsync(int id);
        Task<Response<Sales>> GetSalesAsync();
        Task<Response<Sales>> GetByIdSaleAsync(int id);
        Task<Sales> GetBySaleParametersAsync(Sales sale);
        Task<IEnumerable<Sales>> GetByFiltersAsync(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd);
    }
}
