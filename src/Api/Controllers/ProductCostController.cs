using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using sales_management_api.Controllers;

namespace Api.Controllers
{
    [Route("api/v1")]
    public class ProductCostController(IProductCost productCost) : ApiController
    {
        private readonly IProductCost _productCost = productCost;

        [HttpPost("CreateProductCost")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductCostAsync([FromBody] ProductTotalCostDto productCost)
        {
            var createdProductCost = await _productCost.CreateProductCostAsync(productCost);
            return Response(createdProductCost);
        }
        [HttpGet("GetProductCostById/{id}")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCostByIdAsync([FromRoute] int id)
        {
            var getProductCost = await _productCost.GetProductCostByIdAsync(id);
            return Response(getProductCost);
        }
        [HttpGet("GetAllProductCost")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductCostAsync()
        {
            var getProductCost = await _productCost.GetAllProductCostAsync();
            return Response(getProductCost);
        }
        [HttpPut("UpdateProductCost/{id}")]
        [ProducesResponseType(typeof(Response<ProductTotalCosts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductCostAsync([FromBody] ProductTotalCostDto productCost, [FromRoute] int id)
        {
            var getProductCost = await _productCost.UpdateProductCostAsync(productCost,id);
            return Response(getProductCost);
        }
    }
}