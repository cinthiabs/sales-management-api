using Entities.Entities;

namespace Core.Repositories
{
    public interface ISaleRepository
    {
        Task<Sales> CreateSale(Sales sale);
        Task<bool> CreateSaleList(Sales sale);
        Task<int> UpdateSale(Sales sale);
        Task<int> DeleteSale(int id);
        Task <IEnumerable<Sales>> GetSales();
        Task<Sales> GetByIdSale(int id);
        Task<bool> GetBySaleParameters(Sales sale);
        Task<List<Sales>> GetByFilters(DateTime dataStart, DateTime dataEnd);
    }
}
