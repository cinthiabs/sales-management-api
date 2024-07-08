namespace Application.Interfaces
{
    public interface IUpload
    {
        Task<bool> ReadExcelAsync(Stream stream);
    }
}
