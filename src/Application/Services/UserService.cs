using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository, IAuthentication authentication , IMapper mapper) : IUser
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthentication _authentication = authentication;
        private readonly IMapper _mapper = mapper;
        public void CreatePassword(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));  
        }

        public async Task<Response<bool>> CreateUserAsync(LoginDto loginDto)
        {

            var userExist = await GetUserAsync(loginDto);
            if (userExist.IsSuccess)
                return Response<bool>.Failure(Status.ConflitUser);

            CreatePassword(loginDto.Password, out string passwordHash, out string passwordSalt);

            var createUser = new UserCredentials()
            {
                Username = loginDto.Username,
                Name = loginDto.Name!,
                Email = loginDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Active = true,
                DateCreate = DateTime.Now
            };
            return await _userRepository.CreateUserAsync(createUser);
        }

        public async Task<Response<bool>> DeleteUserAsync(LoginDto loginDto)
        {
            var userResult = await GetUserAsync(loginDto);
            if (userResult.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.DeleteUserAsync(userResult.Data![0].Id);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(LoginDto loginDto)
        {
            var login = _mapper.Map<Login>(loginDto);

            var userResult = await _userRepository.GetUserAsync(login.Email);
            if (userResult.IsFailure)
                return Response<UserCredentials>.Failure(Status.noDatafound);
            
            var user = userResult.Data![0];

            if (!_authentication.VerifyPassword(login.Password, user.PasswordHash!, user.PasswordSalt!))
                return Response<UserCredentials>.Failure(Status.InvalidPassword);
            return userResult;        
        }

        public async Task<Response<bool>> UpdateUserAsync(LoginDto loginDto)
        {
            var userResult = await GetUserAsync(loginDto);
            if (userResult.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.UpdateUserAsync(userResult.Data![0], userResult.Data![0].Id);
        }
    }
}
