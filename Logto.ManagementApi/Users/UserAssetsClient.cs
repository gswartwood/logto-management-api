using System.Net.Http.Headers;
using System.Net.Http.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Users;

public sealed class UserAssetsClient(HttpClient httpClient) : IUserAssetsClient
{
    public async Task<UserAssetServiceStatus> GetServiceStatusAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("user-assets/service-status", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserAssetServiceStatus>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for user-assets/service-status get.");
    }

    public async Task<UserAsset> UploadAsync(Stream file, string fileName, string? contentType = null, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(file);
        if (contentType != null)
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "file", fileName);
        var response = await httpClient.PostAsync("user-assets", content, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserAsset>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for user-assets upload.");
    }
}
