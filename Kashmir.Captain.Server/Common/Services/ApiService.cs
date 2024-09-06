using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Kashmir.Captain.Server.Client
{
	public class ApiService : IApiService
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly IServiceProvider _serviceProvider;
		private bool _isDevTokenRequest;

		public ApiService(IHttpClientFactory clientFactory, IServiceProvider serviceProvider)
		{
			_clientFactory = clientFactory;
			_serviceProvider = serviceProvider;
		}

		public async Task<TResponse?> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var response = await client.GetAsync(url, cancellationToken);
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return default;
			}
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		public async Task<string> PostJsonAsync<T>(string url, T data)
		{
			var httpClient = new HttpClient();
			// Ensure that the url is an absolute URI or set BaseAddress
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
			{
				throw new InvalidOperationException("The request URI must be an absolute URI.");
			}
			var jsonData = JsonConvert.SerializeObject(data);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await httpClient.PostAsync(url, content);
			if (response.IsSuccessStatusCode)
			{
				string responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Unprocessable Entity: {errorContent}");
				throw new HttpRequestException("Unprocessable Entity: " + errorContent);
			}
			else
			{
				response.EnsureSuccessStatusCode();
			}
			return "";
		}

		public async Task<TResponse?> GetAsync<TResponse>(string url, Dictionary<string, object?> queryParameters, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var parameters = queryParameters.ToDictionary(p => p.Key, p => p.Value?.ToString());
			url = QueryHelpers.AddQueryString(url, parameters);
			var response = await client.GetAsync(url, cancellationToken);
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return default;
			}
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		public async Task PostAsync<TRequest>(
			 string url, TRequest request, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			await client.PostAsJsonAsync(url, request, cancellationToken);
		}

		public async Task<TResponse?> PostAsync<TRequest, TResponse>(
			 string url, TRequest request, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var response = await client.PostAsJsonAsync(url, request, cancellationToken);
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		public async Task<TResponse?> PostContentAsync<TResponse>(
	  string url, MultipartFormDataContent content, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var response = await client.PostAsync(url, content, cancellationToken);
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		public async Task PutAsync<TRequest>(
			 string url, TRequest request, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			await client.PutAsJsonAsync(url, request, cancellationToken);
		}

		public async Task<TResponse?> PutAsync<TRequest, TResponse>(
			 string url, TRequest request, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var response = await client.PutAsJsonAsync(url, request, cancellationToken);
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		public async Task DeleteAsync(string url, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			await client.DeleteAsync(url, cancellationToken);
		}

		public async Task<TResponse?> DeleteAsync<TResponse>(
			 string url, CancellationToken cancellationToken = default)
		{
			var client = await GetHttpClientAsync(cancellationToken);
			var response = await client.DeleteAsync(url, cancellationToken);
			return await ReadContentAsync<TResponse>(response, cancellationToken);
		}

		private async Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken)
		{
			var client = _clientFactory.CreateClient();
			client.DefaultRequestVersion = HttpVersion.Version20;
			client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;

			return client;
		}

		private static async Task<T?> ReadContentAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			if (typeof(T) == typeof(string))
			{
				return (T)(object)await response.Content.ReadAsStringAsync(cancellationToken);
			}
			else
			{
				return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
			}
		}
	}
}