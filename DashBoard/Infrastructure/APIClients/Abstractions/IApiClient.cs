namespace DashBoard.Infrastructure.APIClients.Abstractions
{
    public interface IApiClient
    {

        Task<T> GetAsync<T>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<T> PutAsync<T>(string endpoint, T data);
        Task DeleteAsync(string endpoint);
    }
}
