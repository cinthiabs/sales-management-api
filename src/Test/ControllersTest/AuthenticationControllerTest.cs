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
            _mapperMock = new Mock<IMapper>();
            _authenticationMock = new Mock<IAuthentication>();
            _controller = new AuthenticationController(_mapperMock.Object, _authenticationMock.Object);
        }

        [Fact]
        public async Task AuthenticationAsync_ShouldReturnOk_WhenAuthenticationIsSuccessful()
        {
            var (loginDto, login) = SetupLogin("cinthia1234@email.com", "cinthia1234");
            var userCredentials = new UserCredentials { Username = "user" };
            var userResponse = Response<UserCredentials>.Success(userCredentials);

            _authenticationMock.Setup(a => a.AuthenticationAsync(login)).ReturnsAsync(userResponse);
            _mapperMock.Setup(m => m.Map<UserCredentialsDto>(userCredentials)).Returns(new UserCredentialsDto { });

            var result = await _controller.AuthenticationAsync(loginDto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
           // var response = Assert.IsType<Response<UserCredentialsDto>>(result.Value);
           // Assert.Equal("user", response.Data.ToString());
        }

        [Fact]
        public async Task AuthenticationAsync_ShouldReturnBadRequest_WhenAuthenticationFails()
        {
            var (loginDto, login) = SetupLogin("user", "wrongpassword");
            var userResponse = Response<UserCredentials>.Failure(Status.InvalidPassword);

            _authenticationMock.Setup(a => a.AuthenticationAsync(login)).ReturnsAsync(userResponse);

            var result = await _controller.AuthenticationAsync(loginDto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            var response = Assert.IsType<Response<UserCredentials>>(result.Value);
            Assert.Equal("Invalid Password!", response.Message);
        }


        private (LoginDto, Login) SetupLogin(string email, string password)
        {
            var loginDto = new LoginDto { Email = email, Password = password };
            var login = new Login { Email = email, Password = password };

            _mapperMock.Setup(m => m.Map<Login>(loginDto)).Returns(login);

            return (loginDto, login);
        }

    }
}
