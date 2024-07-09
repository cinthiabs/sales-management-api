using Api.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ProductController(IProduct product, IMapper mapper) : ControllerBase
    {
        private readonly IProduct _product = product;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _product.GetProductsAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductsDTO>>(products);
            return Ok(productsDto);
        }

        [HttpGet("GetByIdProduct/{id}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid Product");

            var product = await _product.GetByIdProductAsync(id);
            if (product is null)
                return NotFound();

            var productDto = _mapper.Map<ProductsDTO>(product);
            return Ok(productDto);
        }
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductsDTO productDto)
        {
            if (productDto is null)
                return BadRequest("Product invalid!");

            var product = _mapper.Map<Products>(productDto);
            var productCreated = await _product.CreateProductAsync(product);
            var productCreatedDto = _mapper.Map<ProductsDTO>(productCreated);
            return Ok(productCreatedDto);
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] ProductsDTO productoDto, int id)
        {
            if (productoDto is null)
                return BadRequest("Product invalid!");

            var product  = _mapper.Map<Products>(productoDto);
            product.Id = id;
            bool productUpdated = await _product.UpdateProductAsync(product);
            return productUpdated ? Ok(productUpdated) : BadRequest("Unable to update data!");
        }
        [HttpDelete("DeleteProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (id == 0)
                return BadRequest("Product invalid!");
            bool productDelete = await _product.DeleteProductAsync(id);
            return productDelete ? Ok(productDelete) : BadRequest("Unable to delete data!");
        }

    }
}
