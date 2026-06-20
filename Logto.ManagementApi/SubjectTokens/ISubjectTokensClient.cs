namespace Logto.ManagementApi.SubjectTokens;

public interface ISubjectTokensClient
{
    Task<SubjectToken> CreateAsync(CreateSubjectTokenRequest request, CancellationToken cancellationToken = default);
}
