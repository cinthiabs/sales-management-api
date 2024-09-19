using Api.Controllers;
using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.ControllersTest
{
    public class UserCredentialsControllerTest
    {
        private readonly Mock<IUser> _mockUser;
        private readonly UserCredentialsController _controller;

        public UserCredentialsControllerTest()
        {
            _mockUser = new Mock<IUser>();
            _controller = new UserCredentialsController(_mockUser.Object);
        }
        [Fact]
        public async Task CreateUserAsync_ReturnsOkResult_WhenUserIsCreated()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "password" };
            var response = Response<bool>.Success(true);

            _mockUser.Setup(u => u.CreateUserAsync(loginDto)).ReturnsAsync(response);

            var result = await _controller.CreateUserAsync(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Response<bool>>(okResult.Value);
            Assert.True(returnValue.Data![0]);
        }
        [Fact]
        public async Task CreateUserAsync_ReturnsBadRequest_WhenUserCreationFails()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "00000" };
            var response = Response<bool>.Failure(Status.InvalidPassword);

            _mockUser.Setup(u => u.CreateUserAsync(loginDto)).ReturnsAsync(response);

            var result = await _controller.CreateUserAsync(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<Response<bool>>(badRequestResult.Value);
            Assert.False(returnValue.IsSuccess);
        }
    }

}

