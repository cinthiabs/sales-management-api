namespace Core.Services.Interfaces
{
    public interface IUpload
    {
        Task<bool> ReadExcel(Stream stream);
    }
}
