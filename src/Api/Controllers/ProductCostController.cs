using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ProductCostController(IProductCost productCost) : ControllerBase
    {
        private readonly IProductCost _productCost = productCost;

        [HttpPost("CreateProductCost")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductCostAsync([FromBody] ProductTotalCostDto productCost)
        {
            var createdProductCost = await _productCost.CreateProductCostAsync(productCost);
            if (createdProductCost.IsFailure)
                return BadRequest(createdProductCost);

            return Ok(createdProductCost);
        }
        [HttpPost("GetProductCostById/{id}")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCostByIdAsync([FromBody] int id)
        {
            var getProductCost = await _productCost.GetProductCostByIdAsync(id);
            if (getProductCost.IsFailure)
                return BadRequest(getProductCost);

            return Ok(getProductCost);
        }

    }
}