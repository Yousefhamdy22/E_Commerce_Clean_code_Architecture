using DashBoard.Infrastructure.APIClients.Abstractions;


namespace DashBoard.Infrastructure.APIClients.Implementations
{
    public class ApiClient : IApiClient
    {

        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }

          
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error: {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<T> PutAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error: {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error: {response.StatusCode}");
        }
    }
}
