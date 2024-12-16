using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using sales_management_api.Controllers;

namespace Test.ControllersTest
{
    public class ProductControllerTest
    {
        private readonly Mock<IProduct> _mock;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mock = new Mock<IProduct>();
            _controller = new ProductController(_mock.Object);
        }
        [Fact]
        public async Task GetAllProductsAsync_ReturnsOk_WhenProductsAreFound()
        {
            // Arrange
            var response = new Response<Products> { IsSuccess = true };
            _mock.Setup(s => s.GetProductsAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllProductAsync_ReturnsNotFound_WhenNoProductsFound()
        {
            // Arrange
            var response = new Response<Products> { Code = Status.noDatafound };
            _mock.Setup(s => s.GetProductsAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task CreateProductAsync_ReturnsOk_WhenProductCreated()
        {
            // Arrange
            var response = new Response<Products> { Code = Status.InsertSuccess, IsSuccess = true };
            var productDto = new ProductsDto
            {
                Name = "Product 1",
                Price = 12,
                Details = "Description",
                Active = true,
            };
            _mock.Setup(s => s.CreateProductAsync(productDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateProductAsync(productDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
