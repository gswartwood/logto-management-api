namespace Logto.ManagementApi.OneTimeTokens;

public interface IOneTimeTokensClient
{
    Task<IReadOnlyList<OneTimeToken>> ListAsync(string? email = null, OneTimeTokenStatus? status = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<OneTimeToken> CreateAsync(CreateOneTimeTokenRequest request, CancellationToken cancellationToken = default);
    Task<OneTimeToken> GetAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<OneTimeToken> VerifyAsync(VerifyOneTimeTokenRequest request, CancellationToken cancellationToken = default);
    Task<OneTimeToken> ReplaceStatusAsync(string id, ReplaceOneTimeTokenStatusRequest request, CancellationToken cancellationToken = default);
}
