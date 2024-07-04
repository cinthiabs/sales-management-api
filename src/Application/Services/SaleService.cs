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
        public async Task<Sales> CreateSale(Sales sale)
        {
            var result =  await _saleRepository.CreateSale(sale);
            return result;
        }
        
        public async Task<bool> DeleteSale(int id)
        {
            var record = await _saleRepository.GetByIdSale(id);
            if(record is not null) 
            {
                var rowsAffected = await _saleRepository.DeleteSale(id);
                return rowsAffected;
            }
            return false;
        }

        public async Task<Sales> GetByIdSale(int id)
        {
            var sale = await _saleRepository.GetByIdSale(id);
            return sale;
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            var sales = await _saleRepository.GetSales();
            return sales;
        }
        public async Task<bool> UpdateSale(Sales sale)
        {
            var record = await _saleRepository.GetByIdSale(sale.Id);
            if (record is not null)
            {
                var updated = await _saleRepository.UpdateSale(sale);
                return updated;
            }
            return false;     
        }

        public async Task<bool> CreateSaleList(List<Sales> sale)
        {
            bool result = false;
            

            foreach (var item in sale)
            {
                if( item.Name is not null)
                {
                    var saleExist = await _saleRepository.GetBySaleParameters(item);

                    if (saleExist.Id == 0)
                    {
                        result = await _saleRepository.CreateSaleList(item);
                    }
                    else
                    {
                        result = false;
                    }
                }
                
            }
            return result;
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantity(DateTime dateIni, DateTime dateEnd)
        {
            var relQuantity = await _saleRepository.GetRelQuantity(dateIni, dateEnd);
            return relQuantity;
        }
    }
}
