using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISale
    {
        Task<Response<Sales>> CreateSaleAsync(Sales sale);
        Task<bool> CreateSaleListAsync(List<Sales> sale);
        Task<Response<Sales>> UpdateSaleAsync(Sales sale);
        Task<Response<bool>> DeleteSaleAsync(int id);
        Task<Response<Sales>> GetSalesAsync();
        Task<Response<Sales>> GetByIdSaleAsync(int id);
        Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd);
    }
}
