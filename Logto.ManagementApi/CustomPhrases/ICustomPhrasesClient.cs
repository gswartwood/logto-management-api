namespace Logto.ManagementApi.CustomPhrases;

public interface ICustomPhrasesClient
{
    Task<IReadOnlyList<CustomPhrase>> ListAsync(CancellationToken cancellationToken = default);
    Task<CustomPhrase> GetAsync(string languageTag, CancellationToken cancellationToken = default);
    Task<CustomPhrase> UpsertAsync(string languageTag, Dictionary<string, object> translation, CancellationToken cancellationToken = default);
    Task DeleteAsync(string languageTag, CancellationToken cancellationToken = default);
}
