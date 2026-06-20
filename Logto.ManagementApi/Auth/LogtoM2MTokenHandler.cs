using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace Logto.ManagementApi.Auth;

internal sealed class LogtoM2MTokenHandler(
    IHttpClientFactory httpClientFactory,
    IOptions<ApiClientOptions> options) : DelegatingHandler
{
    internal const string AuthClientName = "Logto_AuthTokenClient";

    private readonly ApiClientOptions _options = options.Value;
    private string? _accessToken;
    private DateTimeOffset _tokenExpiry = DateTimeOffset.MinValue;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(cancellationToken));
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (_accessToken is not null && DateTimeOffset.UtcNow < _tokenExpiry)
            return _accessToken;

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (_accessToken is not null && DateTimeOffset.UtcNow < _tokenExpiry)
                return _accessToken;

            var client = httpClientFactory.CreateClient(AuthClientName);
            var response = await client.PostAsync(
                _options.TokenEndpoint,
                new FormUrlEncodedContent([
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("resource", _options.ApiResource),
                    new KeyValuePair<string, string>("client_id", _options.ManagementApiClientId),
                    new KeyValuePair<string, string>("client_secret", _options.ManagementClientSecret),
                    new KeyValuePair<string, string>("scope", "all")
                ]),
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<LogtoTokenResponse>(cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Logto token endpoint returned an empty response.");

            _accessToken = tokenResponse.AccessToken;
            _tokenExpiry = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 30);
            return _accessToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
