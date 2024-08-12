using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Services
{
    public class Authentication(IUserRepository userRepository, IConfiguration configuration) : IAuthentication
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _config = configuration;
        public async Task<Response<UserCredentials>> AuthenticationAsync(Login login)
        {
            var userExist = await _userRepository.GetUserAsync(login.Username, login.Email);
            if (userExist.IsFailure)
                return userExist;

            var tokenString = GerarTokenJWT();
            var proximoToken = DateTime.Now.AddMinutes(60);
            var createToken = new UserCredentials()
            {
                Username = login.Username,
                Email = login.Email,
                Token = tokenString,
                TokenExpiration = proximoToken,
                LastLogin = DateTime.Now
            };

            return Response<UserCredentials>.Success(createToken);

        }

        private string GerarTokenJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            _ = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
