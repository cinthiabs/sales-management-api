using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController(IProduct product) : ControllerBase
    {
        private readonly IProduct _product = product;

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            var getProducts = await _product.GetProductsAsync();
            if(getProducts.Code == Status.noDatafound)
                return NotFound(getProducts);

            return Ok(getProducts);
        }

        [HttpGet("GetByIdProduct/{id}")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            var productId = await _product.GetByIdProductAsync(id);
            if (productId.IsFailure)
                return NotFound(product);

            return Ok(productId);
        }
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductsDto productDto)
        {
            var productCreated = await _product.CreateProductAsync(productDto);
            if(productCreated.IsFailure)
                return BadRequest(productCreated);

            return Ok(productCreated);
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] ProductsDto productoDto, int id)
        {
            var productUpdated = await _product.UpdateProductAsync(productoDto, id);
            if(productUpdated.IsFailure)
                return BadRequest(productUpdated);

            return Ok(productUpdated);
        }
        [HttpDelete("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var productDelete = await _product.DeleteProductAsync(id);
            if(productDelete.IsFailure)
                return BadRequest(productDelete);
            
            return Ok(productDelete);
        }

    }
}
