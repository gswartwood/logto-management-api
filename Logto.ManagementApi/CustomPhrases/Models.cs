namespace Logto.ManagementApi.CustomPhrases;

public record CustomPhrase(
    string TenantId,
    string Id,
    string LanguageTag,
    Dictionary<string, object> Translation
);
