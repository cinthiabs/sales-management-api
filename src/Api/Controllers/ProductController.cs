using Api.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sales_management_api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController(IProduct product, IMapper mapper) : ControllerBase
    {
        private readonly IProduct _product = product;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(Response<ProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            var getProducts = await _product.GetProductsAsync();
            if(getProducts.Code == Status.noDatafound)
                return NotFound(getProducts);

            var productsDto = _mapper.Map<IEnumerable<ProductsDto>>(getProducts.Data);
            return Ok(productsDto);
        }

        [HttpGet("GetByIdProduct/{id}")]
        [ProducesResponseType(typeof(Response<ProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            var productId = await _product.GetByIdProductAsync(id);
            if (productId.IsFailure)
                return NotFound(product);

            var productDto = _mapper.Map<ProductsDto>(productId?.Data?.FirstOrDefault());
            return Ok(productDto);
        }
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Response<ProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductsDto productDto)
        {
            var mapProduct = _mapper.Map<Products>(productDto);
            var productCreated = await _product.CreateProductAsync(mapProduct);
            if(productCreated.IsFailure)
                return BadRequest(productCreated);

            var productCreatedDto = _mapper.Map<ProductsDto>(productCreated.Data.FirstOrDefault());
            return Ok(productCreatedDto);
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(typeof(Response<ProductsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCostAsync([FromBody] ProductsDto productoDto, int id)
        {
            var mapProduct  = _mapper.Map<Products>(productoDto);
            mapProduct.Id = id;

            var productUpdated = await _product.UpdateProductAsync(mapProduct);
            if(productUpdated.IsFailure)
                return BadRequest(productUpdated);

            var productDto = _mapper.Map<ProductsDto>(productUpdated?.Data?.FirstOrDefault());
            return Ok(Response<ProductsDto>.Success(productDto));
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
