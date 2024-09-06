using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Kashmir.Captain.Server.Client
{
       public interface IApiService
    {
        Task<TResponse?> GetAsync<TResponse>( string url, CancellationToken cancellationToken = default);
        Task<TResponse?> GetAsync<TResponse>( string url, Dictionary<string, object?> queryParameters, CancellationToken cancellationToken = default);
        Task PostAsync<TRequest>( string url, TRequest request, CancellationToken cancellationToken = default);
        Task<TResponse?> PostAsync<TRequest, TResponse>( string url, TRequest request, CancellationToken cancellationToken = default);
        Task PutAsync<TRequest>( string url, TRequest request, CancellationToken cancellationToken = default);
        Task<TResponse?> PutAsync<TRequest, TResponse>( string url, TRequest request, CancellationToken cancellationToken = default);
        Task<TResponse?> DeleteAsync<TResponse>( string url, CancellationToken cancellationToken = default);
        Task DeleteAsync( string url, CancellationToken cancellationToken = default);
        Task<TResponse?> PostContentAsync<TResponse>( string url, MultipartFormDataContent content, CancellationToken cancellationToken = default);
        Task<string> PostJsonAsync<T>(string url, T data);
    }
}