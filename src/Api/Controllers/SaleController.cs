using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.DTOs;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class SaleController(ISale sale, IMapper mapper) : ControllerBase
    {
        private readonly ISale _sale = sale;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllSales")]
        [ProducesResponseType(typeof(IEnumerable<SalesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalesAsync()
        {
            var sales = await _sale.GetSalesAsync();
            if(sales.IsFailure)
                return BadRequest(sales);
            
            var salesDto = _mapper.Map<IEnumerable<SalesDTO>>(sales);
            return Ok(salesDto);
        }
        
        [HttpGet("GetByIdSale/{id}")]
        [ProducesResponseType(typeof(SalesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSaleAsync(int id)
        {
            var sale = await _sale.GetByIdSaleAsync(id);
            if (sale.IsFailure)
                return NotFound(sale);

            var saleDto = _mapper.Map<SalesDTO>(sale.Data.FirstOrDefault());
            return Ok(saleDto);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(typeof(SalesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SalesDTO saleDto)
        {
            var sales = _mapper.Map<Sales>(saleDto);
            var saleCreated = await _sale.CreateSaleAsync(sales);
            if(saleCreated.IsFailure)
                return BadRequest(saleCreated);
            
            var saleCreatedDto = _mapper.Map<CostsDTO>(saleCreated);
            return Ok(saleCreatedDto);
        }

        [HttpPut("UpdateSale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] SalesDTO saleDto, int id)
        {
            var sale = _mapper.Map<Sales>(saleDto);
            sale.Id = id;

            var saleUpdate = await _sale.UpdateSaleAsync(sale);
            if(saleUpdate.IsFailure)
                return BadRequest(saleUpdate);

            return Ok(saleUpdate);
        }

        [HttpDelete("DeleteSale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSaleAsync(int id)
        {
            var saleDelete = await _sale.DeleteSaleAsync(id);
            if(saleDelete.IsFailure)
                return BadRequest(saleDelete);
            
            return Ok(saleDelete);
        }

        [HttpGet("GetRelQuantity")]
        [ProducesResponseType(typeof(IEnumerable<RelQuantity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var saleCreated = await _sale.GetRelQuantityAsync(dateIni, dateEnd);
            return Ok(saleCreated);
        }
    }
}
