﻿using Azure;
using Core.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISale _sale;
        public SaleController(ISale sale)
        {
            _sale = sale;   
        }
        [HttpPost("UploadExcel")]
        [SwaggerOperation(OperationId = "UploadExcel", Description = "Import datas to Sale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file is null && file?.Length == 0)
                return BadRequest("File invalid!");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var resultado = await _sale.ReadExcel(stream);
                
            }
            return Ok(file);
        }
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            var saleCreated = await _sale.GetSales();
            return Ok(saleCreated);
        }

        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale(Sales sale)
        {
            if (sale is null)
                return BadRequest("Sale invalid!");
            var saleCreated = await _sale.CreateSale(sale);
            return Ok(saleCreated);
        }

        [HttpPut("UpdateSale")]
        public async Task<IActionResult> UpdateSale(Sales sale)
        {
            if (sale is null)
                return BadRequest("Sale invalid!");
            var saleCreated = await _sale.UpdateSale(sale);
            return Ok(saleCreated);
        }
        [HttpDelete("DeleteSale")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (id == 0)
                return BadRequest("Sale invalid!");
            var saleCreated = await _sale.DeleteSale(id);
            return Ok(saleCreated);
        }
    }
}
