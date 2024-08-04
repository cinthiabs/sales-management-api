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
            if(products.IsFailure)
                return BadRequest(products);

            var productsDto = _mapper.Map<IEnumerable<ProductsDTO>>(products.Data);
            return Ok(productsDto);
        }

        [HttpGet("GetByIdProduct/{id}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            var product = await _product.GetByIdProductAsync(id);
            if (product.IsFailure)
                return NotFound(product);

            var productDto = _mapper.Map<ProductsDTO>(product.Data.FirstOrDefault());
            return Ok(productDto);
        }
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductsDTO productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            var productCreated = await _product.CreateProductAsync(product);
            if(productCreated.IsFailure)
                return BadRequest(productCreated);

            var productCreatedDto = _mapper.Map<ProductsDTO>(productCreated.Data);
            return Ok(productCreatedDto);
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] ProductsDTO productoDto, int id)
        {
            var product  = _mapper.Map<Products>(productoDto);
            product.Id = id;

            var productUpdated = await _product.UpdateProductAsync(product);
            if(productUpdated.IsFailure)
                return BadRequest(productUpdated);
            
            return Ok(productUpdated);
        }
        [HttpDelete("DeleteProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
