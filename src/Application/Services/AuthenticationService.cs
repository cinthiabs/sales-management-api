using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        public async Task<Response<UserCredentials>> AuthenticationAsync(Login login)
        {
            var userResult = await _userRepository.GetUserAsync(login.Username, login.Email);
            if (userResult.IsFailure)
                return userResult;

            var user = userResult.Data[0];

            if (user.TokenExpiration > DateTime.Now)
            {
                return Response<UserCredentials>.Success(MapToUserCredentials(user));
            }

            var newToken = GenerateJwtToken();
            user.Token = newToken;
            user.TokenExpiration = DateTime.Now.AddMinutes(GetTokenExpirationInMinutes());
            user.LastLogin = DateTime.Now;

            var updateResult = await _userRepository.UpdateUserAsync(user, user.Id);
            if (updateResult.IsFailure)
                return Response<UserCredentials>.Failure(Status.UpdateFailure);

            return Response<UserCredentials>.Success(MapToUserCredentials(user));
        }

        private string GenerateJwtToken()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(GetTokenExpirationInMinutes()), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private int GetTokenExpirationInMinutes()
        {
            return int.TryParse(_config["Jwt:ExpirationInMinutes"], out var minutes) ? minutes : 60;
        }

        private UserCredentials MapToUserCredentials(UserCredentials user)
        {
            return new UserCredentials
            {
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Token = user.Token,
                TokenExpiration = user.TokenExpiration,
                LastLogin = user.LastLogin
            };
        }
    }
}
