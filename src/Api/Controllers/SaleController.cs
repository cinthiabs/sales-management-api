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
        public async Task<IActionResult> GetAllSales()
        {
            var saleCreated = await _sale.GetSales();
            return Ok(saleCreated);
        }
        
        [HttpGet("GetByIdSale")]
        [ProducesResponseType(typeof(Sales), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdSale(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid Sale");
            }
            var saleCreated = await _sale.GetByIdSale(id);
            return Ok(saleCreated);
        }

        [HttpPost("CreateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSale([FromBody] Sales sale)
        {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
            var saleCreated = await _sale.CreateSale(sale);
            return Ok(saleCreated);
        }

        [HttpPut("UpdateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSale([FromBody] Sales sale)
            {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
            bool saleUpdate = await _sale.UpdateSale(sale);
            return saleUpdate ? Ok(saleUpdate) : BadRequest("Unable to update data!");

        }
        [HttpDelete("DeleteSale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (id == 0)
                return BadRequest("Sale invalid!");
            bool saleDelete = await _sale.DeleteSale(id);
            return saleDelete ? Ok(saleDelete) : BadRequest("Unable to delete data!");
        }
        [HttpGet("GetRelQuantity")]
        [ProducesResponseType(typeof(IEnumerable<RelQuantity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRelQuantity(DateTime dateIni, DateTime dateEnd)
        {
            var saleCreated = await _sale.GetRelQuantity(dateIni, dateEnd);
            return Ok(saleCreated);
        }
    }
}
