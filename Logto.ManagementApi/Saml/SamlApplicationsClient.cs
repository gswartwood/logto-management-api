using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Saml;

public sealed class SamlApplicationsClient(HttpClient httpClient) : ISamlApplicationsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<SamlApplication> CreateAsync(CreateSamlApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("saml-applications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplication>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications create.");
    }

    public async Task<SamlApplication> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"saml-applications/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplication>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications get.");
    }

    public async Task<SamlApplication> UpdateAsync(string id, UpdateSamlApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"saml-applications/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplication>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"saml-applications/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task GetCallbackAsync(string id, string? code = null, string? state = null, string? redirectUri = null, string? error = null, string? errorDescription = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (code is not null) query.Append($"code={Uri.EscapeDataString(code)}&");
        if (state is not null) query.Append($"state={Uri.EscapeDataString(state)}&");
        if (redirectUri is not null) query.Append($"redirectUri={Uri.EscapeDataString(redirectUri)}&");
        if (error is not null) query.Append($"error={Uri.EscapeDataString(error)}&");
        if (errorDescription is not null) query.Append($"error_description={Uri.EscapeDataString(errorDescription)}&");
        var escaped = Uri.EscapeDataString(id);
        var url = query.Length > 0 ? $"saml-applications/{escaped}/callback?{query.ToString().TrimEnd('&')}" : $"saml-applications/{escaped}/callback";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<string> GetMetadataAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"saml-applications/{Uri.EscapeDataString(id)}/metadata", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<SamlApplicationSecret> CreateSecretAsync(string id, CreateSamlApplicationSecretRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"saml-applications/{Uri.EscapeDataString(id)}/secrets", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplicationSecret>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications secrets create.");
    }

    public async Task<IReadOnlyList<SamlApplicationSecret>> ListSecretsAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"saml-applications/{Uri.EscapeDataString(id)}/secrets", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplicationSecret[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications secrets list.");
    }

    public async Task DeleteSecretAsync(string id, string secretId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"saml-applications/{Uri.EscapeDataString(id)}/secrets/{Uri.EscapeDataString(secretId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<SamlApplicationSecret> UpdateSecretAsync(string id, string secretId, UpdateSamlApplicationSecretRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"saml-applications/{Uri.EscapeDataString(id)}/secrets/{Uri.EscapeDataString(secretId)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SamlApplicationSecret>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for saml-applications secrets update.");
    }
}
