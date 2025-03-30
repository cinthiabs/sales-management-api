namespace Infrastructure.Cache;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value);
    Task<T?> GetAsync<T>(string key);
}
