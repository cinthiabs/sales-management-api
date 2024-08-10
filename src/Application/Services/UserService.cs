using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Security.Cryptography;

namespace Application.Services
{
    public class UserService(UserRepository userRepository) : IUser
    {
        private readonly IUserRepository _userRepository = userRepository;
        public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<Response<bool>> CreateUserAsync(Login login)
        {
            var userExist = await _userRepository.GetUserAsync(login.Username);
            if (userExist.IsSuccess)
                return Response<bool>.Failure(Status.ConflitUser);

            CreatePassword(login.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var createUser = new UserCredentials()
            {
                Username = login.Username,
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
            var user = await _userRepository.GetUserAsync(login.Username);
            if (user.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.DeleteUserAsync(user.Data![0].Id);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(string username, string password)
        {
            return await _userRepository.GetUserAsync(username);
        }

        public Task<Response<bool>> UpdateUserAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
