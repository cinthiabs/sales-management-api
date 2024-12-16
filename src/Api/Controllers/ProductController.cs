using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [Authorize("Bearer")]
    public class ProductController(IProduct product) : ApiController
    {
        private readonly IProduct _product = product;

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            var getProducts = await _product.GetProductsAsync();
            return Response(getProducts);
        }

        [HttpGet("GetByIdProduct/{id}")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            var productId = await _product.GetByIdProductAsync(id);
            return Response(productId);
        }
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductsDto productDto)
        {
            var productCreated = await _product.CreateProductAsync(productDto);
            return Response(productCreated);
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(typeof(Response<Products>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] ProductsDto productoDto, int id)
        {
            var productUpdated = await _product.UpdateProductAsync(productoDto, id);
            return Response(productUpdated);
        }
        [HttpDelete("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var productDelete = await _product.DeleteProductAsync(id);
            return Response(productDelete); 
        }

    }
}
