using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Dtos;
using AutoMapper;

namespace Application.Services
{
    public class AuthenticationService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, IMapper mapper ) : IAuthentication
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<UserCredentialsDto>> AuthenticationAsync(LoginDto loginDto)
        {
            var mapLogin = _mapper.Map<Login>(loginDto);

            var userResult = await _userRepository.GetUserAsync(mapLogin.Email);
            if (userResult.IsFailure)
                return Response<UserCredentialsDto>.Failure(Status.noDatafound);

            var user = userResult.Data!.FirstOrDefault();
            if (user == null)
                return Response<UserCredentialsDto>.Failure(Status.noDatafound);

            if (!VerifyPassword(mapLogin.Password, user.PasswordHash!, user.PasswordSalt!))
                return Response<UserCredentialsDto>.Failure(Status.InvalidPassword);

            if (user.TokenExpiration <= DateTime.Now || user.TokenExpiration == null)
            {
                user.Token = GenerateJwtToken();
                user.TokenExpiration = DateTime.Now.AddMinutes(GetTokenExpirationInMinutes());
                user.LastLogin = DateTime.Now;

                var updateResult = await _userRepository.UpdateUserAsync(user, user.Id);
                if (updateResult.IsFailure)
                    return Response<UserCredentialsDto>.Failure(Status.UpdateFailure);
            }

            var userCredentialsDto = _mapper.Map<UserCredentialsDto>(user);
            return Response<UserCredentialsDto>.Success(userCredentialsDto);
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using var hmac = new HMACSHA512(saltBytes);
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }

        private string GenerateJwtToken()
        {
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(GetTokenExpirationInMinutes()), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private int GetTokenExpirationInMinutes()
        {
            return _jwtSettings.ExpirationInMinutes > 0 ? _jwtSettings.ExpirationInMinutes : 60;
        }

        //private static UserCredentials MapToUserCredentials(UserCredentials user)
        //{
        //    return new UserCredentials
        //    {
        //        Username = user.Username,
        //        Name = user.Name,
        //        Email = user.Email,
        //        Token = user.Token,
        //        TokenExpiration = user.TokenExpiration,
        //        LastLogin = user.LastLogin,
        //        DateCreate = user.DateCreate,
        //        DateEdit = user.DateEdit
        //    };
        //}
    }
}
