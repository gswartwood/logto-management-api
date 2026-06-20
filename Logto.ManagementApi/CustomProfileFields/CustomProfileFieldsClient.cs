using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.CustomProfileFields;

public sealed class CustomProfileFieldsClient(HttpClient httpClient) : ICustomProfileFieldsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<CustomProfileField>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("custom-profile-fields", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<CustomProfileField>>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile fields list.");
    }

    public async Task<CustomProfileField> CreateAsync(CreateCustomProfileFieldRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("custom-profile-fields", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CustomProfileField>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile field create.");
    }

    public async Task<IReadOnlyList<CustomProfileField>> BatchCreateAsync(IReadOnlyList<CreateCustomProfileFieldRequest> requests, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("custom-profile-fields/batch", requests, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<CustomProfileField>>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile fields batch create.");
    }

    public async Task<CustomProfileField> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var escapedName = Uri.EscapeDataString(name);
        var response = await httpClient.GetAsync($"custom-profile-fields/{escapedName}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CustomProfileField>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile field get.");
    }

    public async Task<CustomProfileField> UpdateAsync(string name, UpdateCustomProfileFieldRequest request, CancellationToken cancellationToken = default)
    {
        var escapedName = Uri.EscapeDataString(name);
        var response = await httpClient.PutAsJsonAsync($"custom-profile-fields/{escapedName}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CustomProfileField>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile field update.");
    }

    public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
    {
        var escapedName = Uri.EscapeDataString(name);
        var response = await httpClient.DeleteAsync($"custom-profile-fields/{escapedName}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CustomProfileField>> UpdateSieOrderAsync(UpdateCustomProfileFieldsSieOrderRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("custom-profile-fields/properties/sie-order", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<CustomProfileField>>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom profile fields sie order update.");
    }
}
