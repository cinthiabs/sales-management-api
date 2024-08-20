using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository, IAuthentication authentication ) : IUser
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthentication _authentication = authentication;
        public void CreatePassword(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));  
        }

        public async Task<Response<bool>> CreateUserAsync(Login login)
        {
            var userExist = await GetUserAsync(login);
            if (userExist.IsSuccess)
                return Response<bool>.Failure(Status.ConflitUser);

            CreatePassword(login.Password, out string passwordHash, out string passwordSalt);

            var createUser = new UserCredentials()
            {
                Username = login.Username,
                Name = login.Name!,
                Email = login.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Active = true,
                DateCreate = DateTime.Now
            };
            return await _userRepository.CreateUserAsync(createUser);
        }

        public async Task<Response<bool>> DeleteUserAsync(Login login)
        {
            var userResult = await GetUserAsync(login);
            if (userResult.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.DeleteUserAsync(userResult.Data![0].Id);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(Login login)
        {
            var userResult = await _userRepository.GetUserAsync(login.Email);
            if (userResult.IsFailure)
                return Response<UserCredentials>.Failure(Status.noDatafound);
            
            var user = userResult.Data![0];

            if (!_authentication.VerifyPassword(login.Password, user.PasswordHash!, user.PasswordSalt!))
                return Response<UserCredentials>.Failure(Status.InvalidPassword);
            return userResult;        
        }

        public async Task<Response<bool>> UpdateUserAsync(Login login)
        {
            var userResult = await GetUserAsync(login);
            if (userResult.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.UpdateUserAsync(userResult.Data![0], userResult.Data![0].Id);
        }
    }
}
