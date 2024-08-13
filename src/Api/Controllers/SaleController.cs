using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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
        [ProducesResponseType(typeof(Response<SalesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalesAsync()
        {
            var getSales = await _sale.GetSalesAsync();
            if(getSales.Code == Status.noDatafound)
                return BadRequest(getSales);
            
            var salesDto = _mapper.Map<IEnumerable<SalesDTO>>(getSales);
            return Ok(salesDto);
        }
        
        [HttpGet("GetByIdSale/{id}")]
        [ProducesResponseType(typeof(Response<SalesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSaleAsync(int id)
        {
            var saleId = await _sale.GetByIdSaleAsync(id);
            if (saleId.IsFailure)
                return NotFound(saleId);

            var saleDto = _mapper.Map<SalesDTO>(saleId.Data.FirstOrDefault());
            return Ok(saleDto);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(typeof(Response<SalesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SalesDTO saleDto)
        {
            var mapSales = _mapper.Map<Sales>(saleDto);
            var saleCreated = await _sale.CreateSaleAsync(mapSales);
            if(saleCreated.IsFailure)
                return BadRequest(saleCreated);
            
            var saleCreatedDto = _mapper.Map<CostsDTO>(saleCreated);
            return Ok(saleCreatedDto);
        }

        [HttpPut("UpdateSale/{id}")]
        [ProducesResponseType(typeof(Response<SalesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] SalesDTO saleDto, int id)
        {
            var mapSale = _mapper.Map<Sales>(saleDto);
            mapSale.Id = id;

            var saleUpdate = await _sale.UpdateSaleAsync(mapSale);
            if(saleUpdate.IsFailure)
                return BadRequest(saleUpdate);

            var saleDTO = _mapper.Map<UserCredentialsDTO>(saleUpdate.Data.FirstOrDefault());
            return Ok(Response<UserCredentialsDTO>.Success(saleDTO));
        }

        [HttpDelete("DeleteSale/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
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
            var saleRel = await _sale.GetRelQuantityAsync(dateIni, dateEnd);
            return Ok(saleRel);
        }
    }
}
