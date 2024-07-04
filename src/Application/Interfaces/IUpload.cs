namespace Application.Interfaces
{
    public interface IUpload
    {
        Task<bool> ReadExcel(Stream stream);
    }
}
