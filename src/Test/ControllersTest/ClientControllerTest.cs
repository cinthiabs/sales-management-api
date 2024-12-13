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
    public class ClientControllerTest
    {
        private readonly Mock<IClient> _mockClient;
        private readonly BaseController _controller;

        public ClientControllerTest()
        {
            _mockClient = new Mock<IClient>();
            _controller = new BaseController(_mockClient.Object);
        }

        [Fact]
        public async Task GetAllClientsAsync_ReturnsOk_WhenClientsAreFound()
        {
            // Arrange
            var response = new Response<Clients> { IsSuccess = true };
            _mockClient.Setup(s => s.GetClientsAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllClientsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllClientsAsync_ReturnsNotFound_WhenNoClientsFound()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.noDatafound };
            _mockClient.Setup(s => s.GetClientsAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllClientsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetByIdClientAsync_ReturnsOk_WhenClientIsFound()
        {
            // Arrange
            var response = new Response<Clients> { IsSuccess = true };
            _mockClient.Setup(s => s.GetByIdClientAsync(It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.GetByIdClientAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetByIdClientAsync_ReturnsNotFound_WhenClientNotFound()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.noDatafound, IsSuccess = false };
            _mockClient.Setup(s => s.GetByIdClientAsync(It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.GetByIdClientAsync(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateClientAsync_ReturnsOk_WhenClientCreated()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.InsertSuccess, IsSuccess = true };
            var clientDto = new ClientDto 
            { 
                Name = "Cinthia", 
                Phone ="11999555",
                Location="SP",
                Active = true,
            };
            _mockClient.Setup(s => s.CreateClientAsync(clientDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateClientAsync(clientDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateClientAsync_ReturnsBadRequest_WhenClientCreationFails()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.InsertFailure, IsSuccess = false };
            var clientDto = new ClientDto
            {
                Name = "Cinthia",
                Phone = "11999555",
                Location = "SP",
            };
            _mockClient.Setup(s => s.CreateClientAsync(clientDto)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateClientAsync(clientDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsOk_WhenClientUpdated()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.UpdatedSuccess, IsSuccess = true };
            var clientDto = new ClientDto
            {
                Name = "Cinthia Barbosa",
                Phone = "11999555",
                Location = "SP",
                Active = true,
            };
            _mockClient.Setup(s => s.UpdateClientAsync(clientDto, It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateClientAsync(clientDto, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateClientAsync_ReturnsBadRequest_WhenClientUpdateFails()
        {
            // Arrange
            var response = new Response<Clients> { Code = Status.UpdateFailure, IsSuccess = false };
            var clientDto = new ClientDto
            {
                Name = "Cinthia",
                Phone = "11999555",
                Location = "SP",
                Active = false,
            };
            _mockClient.Setup(s => s.UpdateClientAsync(clientDto, It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateClientAsync(clientDto, 1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsOk_WhenClientDeleted()
        {
            // Arrange
            var response = new Response<bool> { IsSuccess = true };
            _mockClient.Setup(s => s.DeleteClientsAsync(It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteClientAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteClientAsync_ReturnsBadRequest_WhenClientDeletionFails()
        {
            // Arrange
            var response = new Response<bool> { IsSuccess = false, Code = Status.DeleteFailure };
            _mockClient.Setup(s => s.DeleteClientsAsync(It.IsAny<int>())).ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteClientAsync(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
    }
}
