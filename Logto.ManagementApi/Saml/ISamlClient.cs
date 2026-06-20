namespace Logto.ManagementApi.Saml;

public interface ISamlClient
{
    Task GetAuthnAsync(string id, string samlRequest, string? signature = null, string? sigAlg = null, string? relayState = null, CancellationToken cancellationToken = default);
    Task CreateAuthnAsync(string id, CreateSamlAuthnRequest request, CancellationToken cancellationToken = default);
}
