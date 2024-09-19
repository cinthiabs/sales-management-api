using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IClientRepository clientRepository, IMapper mapper) : ISale
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<Sales>> CreateSaleAsync(SalesDto saleDto)
        {
            var mapSale = _mapper.Map<Sales>(saleDto);
            return await _saleRepository.CreateSaleAsync(mapSale);
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
        public async Task<Response<Sales>> UpdateSaleAsync(SalesDto saleDto, int id)
        {
            var mapSale = _mapper.Map<Sales>(saleDto);
            mapSale.Id = id;

            var existSale = await _saleRepository.GetByIdSaleAsync(mapSale.Id);
            if (existSale.IsSuccess)
            {
                var updated = await _saleRepository.UpdateSaleAsync(mapSale);
                return updated;
            }
            return existSale;     
        }

        public async Task<bool> CreateSaleListAsync(List<Sales> sale)
        {
            foreach (var item in sale.Where(s => s.Name is not null))
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
