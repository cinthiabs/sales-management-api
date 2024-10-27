using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IZipCode
    {
        Task<Response<AddressDto>> GetZipCodeAsync(string zipcode);
    }
}
