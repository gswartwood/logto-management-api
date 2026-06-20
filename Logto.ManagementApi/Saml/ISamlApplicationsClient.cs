namespace Logto.ManagementApi.Saml;

public interface ISamlApplicationsClient
{
    Task<SamlApplication> CreateAsync(CreateSamlApplicationRequest request, CancellationToken cancellationToken = default);
    Task<SamlApplication> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<SamlApplication> UpdateAsync(string id, UpdateSamlApplicationRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task GetCallbackAsync(string id, string? code = null, string? state = null, string? redirectUri = null, string? error = null, string? errorDescription = null, CancellationToken cancellationToken = default);
    Task<string> GetMetadataAsync(string id, CancellationToken cancellationToken = default);
    Task<SamlApplicationSecret> CreateSecretAsync(string id, CreateSamlApplicationSecretRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SamlApplicationSecret>> ListSecretsAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteSecretAsync(string id, string secretId, CancellationToken cancellationToken = default);
    Task<SamlApplicationSecret> UpdateSecretAsync(string id, string secretId, UpdateSamlApplicationSecretRequest request, CancellationToken cancellationToken = default);
}
