using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IClientRepository clientRepository) : ISale
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<Response<Sales>> CreateSaleAsync(Sales sale)
        {
            return await _saleRepository.CreateSaleAsync(sale);
        }
        public async Task<Response<bool>> DeleteSaleAsync(int id)
        {
            var existSale = await _saleRepository.GetByIdSaleAsync(id);
            if(existSale.IsSuccess) 
            {
                var deleteSale = await _saleRepository.DeleteSaleAsync(id);
                return deleteSale;
            }
            return Response<bool>.Failure(Status.noDatafound);
        }

        public async Task<Response<Sales>> GetByIdSaleAsync(int id)
        {
            return await _saleRepository.GetByIdSaleAsync(id);
        }

        public async Task<Response<Sales>> GetSalesAsync()
        {
            var sales = await _saleRepository.GetSalesAsync();
            return sales;
        }
        public async Task<Response<Sales>> UpdateSaleAsync(Sales sale)
        {
            var existSale = await _saleRepository.GetByIdSaleAsync(sale.Id);
            if (existSale.IsSuccess)
            {
                var updated = await _saleRepository.UpdateSaleAsync(sale);
                return updated;
            }
            return existSale;     
        }

        public async Task<bool> CreateSaleListAsync(List<Sales> sales)
        {
            foreach (var item in sales.Where(s => s.Name is not null))
            {
                var saleExist = await _saleRepository.GetBySaleParametersAsync(item);
                if (saleExist.Id != 0)
                    continue;

                var product = await _productRepository.GetByNameProductAsync(item.Name);
                if (product is not null)
                    item.IdProduct = product.Id;

                var client = await _clientRepository.GetClientByNameAsync(item.Details);
                if (client is not null)
                    item.IdClient = client.Id;

                var data = await _saleRepository.CreateSaleListAsync(item);
                if (data.IsFailure)
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<RelQuantity>> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var relQuantity = await _saleRepository.GetRelQuantityAsync(dateIni, dateEnd);
            return relQuantity;
        }
    }
}
