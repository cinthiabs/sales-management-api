using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(Sales sale);
        Task<bool> UpdateSale(Sales sale);
        Task<bool> DeleteSale(int id);
        Task <IEnumerable<Sales>> GetSales();
        Task<Sales> GetByIdSale(int id);
        Task<Sales> GetBySaleParameters(Sales sale);
        Task<IEnumerable<Sales>> GetByFilters(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd);
    }
}
