using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUpload
    {
        Task<Response<bool>> ReadExcelAsync(Stream stream);
    }
}
