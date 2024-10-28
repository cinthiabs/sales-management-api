using Api.Controllers;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.ControllersTest
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAuthentication> _authenticationMock;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _authenticationMock = new Mock<IAuthentication>();
            _controller = new AuthenticationController(_authenticationMock.Object);
        }

        [Fact]
        public async Task AuthenticationAsync_ReturnsOkResult_WhenUserIsAuthenticated()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "password" };
            var userCredentialsDto = ReturnValidResult();
            var response = Response<UserCredentialsDto>.Success(userCredentialsDto);

            _authenticationMock.Setup(auth => auth.AuthenticationAsync(loginDto))
                .ReturnsAsync(response);

            var result = await _controller.AuthenticationAsync(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Response<UserCredentialsDto>>(okResult.Value);
            Assert.Equal(userCredentialsDto.Username, returnValue.Data![0].Username);
        }

        [Fact]
        public async Task AuthenticationAsync_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "0000" };
            var response = Response<UserCredentialsDto>.Failure(Status.noDatafound);

            _authenticationMock.Setup(auth => auth.AuthenticationAsync(loginDto))
                .ReturnsAsync(response);

            var result = await _controller.AuthenticationAsync(loginDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AuthenticationAsync_ReturnsBadRequest_WhenAuthenticationFails()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "", Password = "" };
            var response = Response<UserCredentialsDto>.Failure(Status.InvalidPassword);

            _authenticationMock.Setup(auth => auth.AuthenticationAsync(loginDto))
                .ReturnsAsync(response);

            var result = await _controller.AuthenticationAsync(loginDto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
        private static UserCredentialsDto ReturnValidResult()
        {
            var user = new UserCredentialsDto
            {
                Active = true,
                Name = "Test",
                Email = "test@test.com",
                Username = "User001",
                Token = "VGhpcyBpcyBhIHRlc3Qgc3RyaW5nIGVuY29kZWRpbiBCz",
                TokenExpiration = DateTime.Now,
                LastLogin = DateTime.Now
            };
            return user;
        }
    }
}
