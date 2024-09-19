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

        public async Task<Response<UserProfile>> GetByIdUserProfileAsync(int id)
        {
            return await _userProfileRepository.GetByIdUserProfileAsync(id);
        }

        public async Task<Response<UserProfile>> GetAllUserProfileAsync()
        {
            return await _userProfileRepository.GetUserProfileAsync();
        }

        public async Task<Response<UserProfile>> UpdateUserProfileAsync(UserProfileDto profileDto, int Id)
        {
            var mapUser = _mapper.Map<UserProfile>(profileDto);
            mapUser.Id = Id;

            var existUser = await _userProfileRepository.GetByIdUserProfileAsync(mapUser.Id);
            if (existUser.IsSuccess)
            {
                var updated = await _userProfileRepository.UpdateUserProfileAsync(mapUser);
                return updated;
            }
            return existUser;
        }
    }
}
