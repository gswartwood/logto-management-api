using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Hooks;

[JsonConverter(typeof(JsonStringEnumConverter<HookEvent>))]
public enum HookEvent
{
    PostRegister,
    PostSignIn,
    PostSignInAdaptiveMfaTriggered,
    PostResetPassword,
    [JsonStringEnumMemberName("User.Created")] UserCreated,
    [JsonStringEnumMemberName("User.Deleted")] UserDeleted,
    [JsonStringEnumMemberName("User.Data.Updated")] UserDataUpdated,
    [JsonStringEnumMemberName("User.SuspensionStatus.Updated")] UserSuspensionStatusUpdated,
    [JsonStringEnumMemberName("Role.Created")] RoleCreated,
    [JsonStringEnumMemberName("Role.Deleted")] RoleDeleted,
    [JsonStringEnumMemberName("Role.Data.Updated")] RoleDataUpdated,
    [JsonStringEnumMemberName("Role.Scopes.Updated")] RoleScopesUpdated,
    [JsonStringEnumMemberName("Scope.Created")] ScopeCreated,
    [JsonStringEnumMemberName("Scope.Deleted")] ScopeDeleted,
    [JsonStringEnumMemberName("Scope.Data.Updated")] ScopeDataUpdated,
    [JsonStringEnumMemberName("Organization.Created")] OrganizationCreated,
    [JsonStringEnumMemberName("Organization.Deleted")] OrganizationDeleted,
    [JsonStringEnumMemberName("Organization.Data.Updated")] OrganizationDataUpdated,
    [JsonStringEnumMemberName("Organization.Membership.Updated")] OrganizationMembershipUpdated,
    [JsonStringEnumMemberName("OrganizationRole.Created")] OrganizationRoleCreated,
    [JsonStringEnumMemberName("OrganizationRole.Deleted")] OrganizationRoleDeleted,
    [JsonStringEnumMemberName("OrganizationRole.Data.Updated")] OrganizationRoleDataUpdated,
    [JsonStringEnumMemberName("OrganizationRole.Scopes.Updated")] OrganizationRoleScopesUpdated,
    [JsonStringEnumMemberName("OrganizationScope.Created")] OrganizationScopeCreated,
    [JsonStringEnumMemberName("OrganizationScope.Deleted")] OrganizationScopeDeleted,
    [JsonStringEnumMemberName("OrganizationScope.Data.Updated")] OrganizationScopeDataUpdated,
    [JsonStringEnumMemberName("Identifier.Lockout")] IdentifierLockout,
}

[JsonConverter(typeof(JsonStringEnumConverter<HookLogResult>))]
public enum HookLogResult { Success, Error }

public record HookConfig(
    string Url,
    Dictionary<string, string>? Headers,
    double? Retries);

public record HookExecutionStats(double SuccessCount, double RequestCount);

public record Hook(
    string TenantId,
    string Id,
    string Name,
    HookEvent? Event,
    IReadOnlyList<HookEvent> Events,
    HookConfig Config,
    string SigningKey,
    bool Enabled,
    double CreatedAt,
    HookExecutionStats? ExecutionStats);

public record CreateHookRequest(
    HookConfig Config,
    string? Name,
    IReadOnlyList<HookEvent>? Events,
    bool? Enabled);

public record UpdateHookRequest(
    string? Name,
    IReadOnlyList<HookEvent>? Events,
    HookConfig? Config,
    bool? Enabled);

public record TestHookRequest(
    IReadOnlyList<HookEvent> Events,
    HookConfig Config);

public record HookLogPayload(
    string Key,
    HookLogResult Result,
    JsonElement? Error,
    string? Ip,
    string? UserAgent,
    JsonElement? UserAgentParsed,
    string? UserId,
    string? ApplicationId,
    string? SessionId,
    Dictionary<string, object>? Params);

public record HookRecentLog(string Id, string Key, HookLogPayload Payload, double CreatedAt);

public record ListHookRecentLogsRequest(
    string? LogKey = null,
    bool? EnableCap = null,
    string? StartTime = null,
    string? EndTime = null,
    int? Page = null,
    int? PageSize = null);
