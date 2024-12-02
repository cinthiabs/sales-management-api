using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]

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
        [HttpGet("GetProductCostById/{id}")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCostByIdAsync([FromRoute] int id)
        {
            var getProductCost = await _productCost.GetProductCostByIdAsync(id);
            if (getProductCost.IsFailure)
                return BadRequest(getProductCost);

            return Ok(getProductCost);
        }
        [HttpGet("GetAllProductCost")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductCostAsync()
        {
            var getProductCost = await _productCost.GetAllProductCostAsync();
            if (getProductCost.IsFailure)
                return BadRequest(getProductCost);

            return Ok(getProductCost);
        }
        [HttpPut("UpdateProductCost/{id}")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductCostAsync([FromBody] ProductTotalCostDto productCost, [FromRoute] int id)
        {
            var getProductCost = await _productCost.UpdateProductCostAsync(productCost,id);
            if (getProductCost.IsFailure)
                return BadRequest(getProductCost);

            return Ok(getProductCost);
        }
    }
}