using Azure;
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
                return resultado ? Ok("Upload completed successfully!") : Conflict("Sale already exists!");
            }
        }
        [HttpGet("GetAllSales")]
        [ProducesResponseType(typeof(IEnumerable<Sales>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSales()
        {
            var saleCreated = await _sale.GetSales();
            return Ok(saleCreated);
        }
        
        [HttpGet("GetByIdSale")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Sales), StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSale([FromBody] Sales sale)
        {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
            var saleCreated = await _sale.CreateSale(sale);
            return Ok(saleCreated);
        }

        [HttpPut("UpdateSale")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSale([FromBody] Sales sale)
            {
            if (sale is null)
                return BadRequest("Sale invalid!");
            
            bool saleUpdate = await _sale.UpdateSale(sale);
            return saleUpdate ? Ok(saleUpdate) : BadRequest("Unable to update data!");

        }
        [HttpDelete("DeleteSale/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (id == 0)
                return BadRequest("Sale invalid!");
            bool saleDelete = await _sale.DeleteSale(id);
            return saleDelete ? Ok(saleDelete) : BadRequest("Unable to delete data!");
        }
    }
}
