namespace Logto.ManagementApi;

public class ApiClientOptions
{
    /// <summary>Logto Cloud tenant ID. Used to derive all URLs when <see cref="BaseUrl"/> is not set.</summary>
    public string? TenantId { get; set; }

    /// <summary>Base URL of a self-hosted Logto OSS instance (e.g. "https://auth.example.com"). Takes precedence over <see cref="TenantId"/>.</summary>
    public string? BaseUrl { get; set; }

    public string ManagementApiClientId { get; set; } = string.Empty;
    public string ManagementClientSecret { get; set; } = string.Empty;

    private string Origin => BaseUrl is { Length: > 0 } url
        ? url.TrimEnd('/')
        : $"https://{TenantId}.logto.app";

    internal string TokenEndpoint => $"{Origin}/oidc/token";
    
    internal string ApiResource => BaseUrl is { Length: > 0 } url
        ? "https://default.logto.app/api"
        : $"{Origin}/api";
    
    internal string BaseAddress => $"{Origin}/api/";
}
