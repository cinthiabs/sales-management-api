using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAllSales")]
        [ProducesResponseType(typeof(IEnumerable<Sales>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSalesAsync()
        {
            var saleCreated = await _sale.GetSalesAsync();
            return Ok(saleCreated);
        }
        
        [HttpGet("GetByIdSale")]
        [ProducesResponseType(typeof(Sales), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSaleAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid Sale");
            }
            var saleCreated = await _sale.GetByIdSaleAsync(id);
            return Ok(saleCreated);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] Sales sale)
        {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
            var saleCreated = await _sale.CreateSaleAsync(sale);
            return Ok(saleCreated);
        }

        [HttpPut("UpdateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync([FromBody] Sales sale)
            {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
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
        public async Task<IActionResult> GetRelQuantityAsync(DateTime dateIni, DateTime dateEnd)
        {
            var saleCreated = await _sale.GetRelQuantityAsync(dateIni, dateEnd);
            return Ok(saleCreated);
        }
    }
}
