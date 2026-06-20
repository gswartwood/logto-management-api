namespace Logto.ManagementApi.Resources;

public record ResourceScope(
    string TenantId,
    string Id,
    string ResourceId,
    string Name,
    string? Description,
    double CreatedAt);

public record Resource(
    string TenantId,
    string Id,
    string Name,
    string Indicator,
    bool IsDefault,
    double AccessTokenTtl,
    IReadOnlyList<ResourceScope>? Scopes = null);

public record CreateResourceRequest(string Name, string Indicator, double? AccessTokenTtl = null);

public record UpdateResourceRequest(string? Name = null, double? AccessTokenTtl = null);

public record UpdateResourceIsDefaultRequest(bool IsDefault);

public record CreateResourceScopeRequest(string Name, string? Description = null);

public record UpdateResourceScopeRequest(string? Name = null, string? Description = null);
