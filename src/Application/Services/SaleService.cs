using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class SaleService : ISale
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository saleRepository )
        {
            _saleRepository = saleRepository;
        }
        public async Task<Sales> CreateSaleAsync(Sales sale)
        {
            var result =  await _saleRepository.CreateSaleAsync(sale);
            return result;
        }
        
        public async Task<bool> DeleteSaleAsync(int id)
        {
            var record = await _saleRepository.GetByIdSaleAsync(id);
            if(record is not null) 
            {
                var rowsAffected = await _saleRepository.DeleteSaleAsync(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Sales> GetByIdSaleAsync(int id)
        {
            var sale = await _saleRepository.GetByIdSaleAsync(id);
            return sale;
        }

        public async Task<IEnumerable<Sales>> GetSalesAsync()
        {
            var sales = await _saleRepository.GetSalesAsync();
            return sales;
        }
        public async Task<bool> UpdateSaleAsync(Sales sale)
        {
            var record = await _saleRepository.GetByIdSaleAsync(sale.Id);
            if (record is not null)
            {
                var updated = await _saleRepository.UpdateSaleAsync(sale);
                return updated;
            }
            return false;     
        }

        public async Task<bool> CreateSaleListAsync(List<Sales> sale)
        {
            bool result = false;
            

            foreach (var item in sale)
            {
                if( item.Name is not null)
                {
                    var saleExist = await _saleRepository.GetBySaleParametersAsync(item);

                    if (saleExist.Id == 0)
                    {
                        result = await _saleRepository.CreateSaleListAsync(item);
                    }
                    else
                    {
                        result = false;
                    }
                }
                
            }
            return result;
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var relQuantity = await _saleRepository.GetRelQuantityAsync(dateIni, dateEnd);
            return relQuantity;
        }
    }
}
