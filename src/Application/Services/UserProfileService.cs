using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper) : IUserProfile
    {
        private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<UserProfile>> GetByUsernameProfileAsync(string username)
        {
            return await _userProfileRepository.GetByUsernameProfileAsync(username);
        }

        public async Task<Response<UserProfile>> GetAllUserProfileAsync()
        {
            return await _userProfileRepository.GetUserProfileAsync();
        }

        public async Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfileDto profileDto, string username)
        {
            var mapUser = _mapper.Map<UserProfile>(profileDto);
            mapUser.Username = username;

            var existUser = await _userProfileRepository.GetByUsernameProfileAsync(mapUser.Username);
            if (existUser.IsSuccess)
            {
                var updated = await _userProfileRepository.UpdateUserProfileAsync(mapUser);
                return updated;
            }
            return existUser;
        }
    }
}
