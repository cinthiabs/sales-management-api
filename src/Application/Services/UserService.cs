using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository) : IUser
    {
        private readonly IUserRepository _userRepository = userRepository;
        public void CreatePassword(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))); // Converte o hash para Base64
        }

        public async Task<Response<bool>> CreateUserAsync(Login login)
        {
            var userExist = await _userRepository.GetUserAsync(login.Username, login.Email);
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
            var user = await _userRepository.GetUserAsync(login.Username, login.Email);
            if (user.IsFailure)
                return Response<bool>.Failure(Status.noDatafound);

            return await _userRepository.DeleteUserAsync(user.Data![0].Id);
        }

        public async Task<Response<UserCredentials>> GetUserAsync(string username, string password)
        {
            //return await _userRepository.GetUserAsync(username, );
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateUserAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
