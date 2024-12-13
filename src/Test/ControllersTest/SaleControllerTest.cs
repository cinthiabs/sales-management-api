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
    public class SaleControllerTest
    {
        private readonly Mock<ISale> _mockSale;
        private readonly SaleController _controller;

        public SaleControllerTest()
        {
            _mockSale = new Mock<ISale>();
            _controller = new SaleController(_mockSale.Object);
        }
        [Fact]
        public async Task GetAllSalesAsync_ReturnsOk_WhenSalesAreFound()
        {
            // Arrange
            var response = new Response<Sales> { IsSuccess = true };
            _mockSale.Setup(s => s.GetSalesAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllSalesAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllSaleAsync_ReturnsNotFound_WhenNoSalesFound()
        {
            // Arrange
            var response = new Response<Sales> { Code = Status.noDatafound };
            _mockSale.Setup(s => s.GetSalesAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllSalesAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task CreateSaleAsync_ReturnsOk_WhenSaleCreated()
        {
            // Arrange
            var response = new Response<Sales> { Code = Status.InsertSuccess, IsSuccess = true };
            var saleDto = new SalesDto
            {
               IdClient = 1,
               IdProduct = 1,
               DateSale = DateTime.Now,
               Name = "Sale 1",
               Price = 12,
               Details = "Description",
               Pay = Situation.Pago

            };
            _mockSale.Setup(s => s.CreateSaleAsync(saleDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateSaleAsync(saleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

    }
}
