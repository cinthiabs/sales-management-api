using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISale
    {
        Task<Response<Sales>> CreateSaleAsync(SalesDto sale);
        Task<bool> CreateSaleListAsync(List<Sales> sale);
        Task<Response<Sales>> UpdateSaleAsync(SalesDto sale, int id);
        Task<Response<bool>> DeleteSaleAsync(int id);
        Task<Response<Sales>> GetSalesAsync();
        Task<Response<Sales>> GetByIdSaleAsync(int id);
        Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd);
    }
}
