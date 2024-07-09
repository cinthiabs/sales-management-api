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
            var salesDto = _mapper.Map<IEnumerable<SalesDTO>>(sales);
            return Ok(salesDto);
        }
        
        [HttpGet("GetByIdSale/${id}")]
        [ProducesResponseType(typeof(SalesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSaleAsync(int id)
        {
            if(id == 0)
                return BadRequest("Invalid Sale");

            var sale = await _sale.GetByIdSaleAsync(id);
            if (sale is null)
                return NotFound();

            var saleDto = _mapper.Map<SalesDTO>(sale);
            return Ok(saleDto);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(typeof(SalesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SalesDTO saleDto)
        {
            if (saleDto is null)
                return BadRequest("Sale invalid!");

            var sales = _mapper.Map<Sales>(saleDto);
            var saleCreated = await _sale.CreateSaleAsync(sales);
            var saleCreatedDto = _mapper.Map<CostsDTO>(saleCreated);
            return Ok(saleCreatedDto);
        }

        [HttpPut("UpdateSale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] SalesDTO saleDto, int id)
        {
            if (saleDto is null)
                return BadRequest("Sale invalid!");

            var sale = _mapper.Map<Sales>(saleDto);
            sale.Id = id;
            bool saleUpdate = await _sale.UpdateSaleAsync(sale);
            return saleUpdate ? Ok(saleUpdate) : BadRequest("Unable to update data!");
        }

        [HttpDelete("DeleteSale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSaleAsync(int id)
        {
            if (id == 0)
                return BadRequest("Sale invalid!");
            bool saleDelete = await _sale.DeleteSaleAsync(id);
            return saleDelete ? Ok(saleDelete) : BadRequest("Unable to delete data!");
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
