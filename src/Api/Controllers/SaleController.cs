﻿using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class SaleController(ISale sale, IMapper mapper) : ControllerBase
    {
        private readonly ISale _sale = sale;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllSales")]
        [ProducesResponseType(typeof(Response<Sales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalesAsync()
        {
            var getSales = await _sale.GetSalesAsync();
            if(getSales.Code == Status.noDatafound)
                return BadRequest(getSales);
            
            return Ok(getSales);
        }
        
        [HttpGet("GetByIdSale/{id}")]
        [ProducesResponseType(typeof(Response<Sales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSaleAsync(int id)
        {
            var saleId = await _sale.GetByIdSaleAsync(id);
            if (saleId.IsFailure)
                return NotFound(saleId);

            return Ok(saleId);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(typeof(Response<Sales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SalesDto saleDto)
        {
            var mapSales = _mapper.Map<Sales>(saleDto);
            var saleCreated = await _sale.CreateSaleAsync(mapSales);
            if(saleCreated.IsFailure)
                return BadRequest(saleCreated);
            
            return Ok(saleCreated);
        }

        [HttpPut("UpdateSale/{id}")]
        [ProducesResponseType(typeof(Response<Sales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] SalesDto sale, int id)
        {
            var mapSale = _mapper.Map<Sales>(sale);
            mapSale.Id = id;

            var saleUpdate = await _sale.UpdateSaleAsync(mapSale);
            if(saleUpdate.IsFailure)
                return BadRequest(saleUpdate);

            return Ok(saleUpdate);
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
