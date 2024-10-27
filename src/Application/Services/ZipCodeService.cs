using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ZipCodeService(IZipCodeRepository zipcodeRepository, IMapper mapper) : IZipCode
    {
        private readonly IZipCodeRepository _zipcode = zipcodeRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<AddressDto>> GetZipCodeAsync(string zipcode)
        {
            var response = await _zipcode.GetZipCodeAsync(zipcode);

            if (response.IsFailure)
                return Response<AddressDto>.Failure(response.Code.Value);

            var addressDto = _mapper.Map<AddressDto>(response.Data.FirstOrDefault());
            return Response<AddressDto>.Success(addressDto);
        }
    }
}
