namespace Logto.ManagementApi.Authn;

public interface IAuthnClient
{
    Task<HasuraAuthClaims> GetHasuraAuthAsync(string resource, string? unauthorizedRole = null, CancellationToken cancellationToken = default);
    Task AssertSingleSignOnSamlAsync(string connectorId, AssertSamlRequest request, CancellationToken cancellationToken = default);
}
