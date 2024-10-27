using System.Text.Json;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class ZipCodeRepository(HttpClient httpClient) : IZipCodeRepository
    {
        private readonly HttpClient _httpClient = httpClient;
        internal const string WsZipcodeBaseUrl = "https://viacep.com.br/ws/";

        public async Task<Response<ZipCode>> GetZipCodeAsync(string zipcode)
        {
            var url = $"{WsZipcodeBaseUrl}{zipcode}/json";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return Response<ZipCode>.Failure(Status.noDatafound);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var zipCodeData = JsonSerializer.Deserialize<ZipCode>(jsonResponse);

                return zipCodeData != null
                    ? Response<ZipCode>.Success(zipCodeData)
                    : Response<ZipCode>.Failure(Status.InternalError);
            }
            catch 
            {
                return Response<ZipCode>.Failure(Status.InternalError);
            }
        }
    }
}
