using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IZipCodeRepository
    {
        Task<Response<ZipCode>> GetZipCodeAsync(string zipcode);
    }
}
