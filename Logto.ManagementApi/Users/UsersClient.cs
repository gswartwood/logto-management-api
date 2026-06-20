using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Users;

public sealed class UsersClient(HttpClient httpClient) : IUsersClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<User>> ListAsync(int? page = null, int? pageSize = null, string? search = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page.HasValue) query.Append($"page={page}&");
        if (pageSize.HasValue) query.Append($"page_size={pageSize}&");
        if (search != null) query.Append($"search={Uri.EscapeDataString(search)}&");
        var url = query.Length > 0 ? $"users?{query.ToString().TrimEnd('&')}" : "users";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("users", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users.");
    }

    public async Task<User> GetAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}.");
    }

    public async Task<User> UpdateAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}.");
    }

    public async Task DeleteAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<Dictionary<string, object>> GetCustomDataAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/custom-data", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<Dictionary<string, object>> UpdateCustomDataAsync(string userId, UpdateUserCustomDataRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/custom-data", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<UserLogtoConfigs> GetLogtoConfigsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/logto-configs", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserLogtoConfigs>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/logto-configs.");
    }

    public async Task<UserLogtoConfigs> UpdateLogtoConfigsAsync(string userId, UpdateUserLogtoConfigsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/logto-configs", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserLogtoConfigs>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/logto-configs.");
    }

    public async Task<UserProfile> UpdateProfileAsync(string userId, UpdateUserProfileRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/profile", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserProfile>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/profile.");
    }

    public async Task UpdatePasswordAsync(string userId, UpdateUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/password", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task UpdatePasswordExpirationAsync(string userId, UpdateUserPasswordExpirationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/password/expiration", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task VerifyPasswordAsync(string userId, VerifyUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/password/verify", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<UserHasPasswordResult> GetHasPasswordAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/has-password", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserHasPasswordResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/has-password.");
    }

    public async Task<User> UpdateIsSuspendedAsync(string userId, UpdateUserIsSuspendedRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/is-suspended", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/is-suspended.");
    }

    public async Task<IReadOnlyList<UserRole>> ListRolesAsync(string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page.HasValue) query.Append($"page={page}&");
        if (pageSize.HasValue) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"users/{Uri.EscapeDataString(userId)}/roles?{query.ToString().TrimEnd('&')}" : $"users/{Uri.EscapeDataString(userId)}/roles";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserRole[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task AssignRolesAsync(string userId, AssignUserRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceRolesAsync(string userId, AssignUserRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteRoleAsync(string userId, string roleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/roles/{Uri.EscapeDataString(roleId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<UserIdentityDetail> GetIdentityAsync(string userId, string target, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/identities/{Uri.EscapeDataString(target)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserIdentityDetail>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/identities/{target}.");
    }

    public async Task<Dictionary<string, SocialIdentityEntry>> ReplaceIdentityAsync(string userId, string target, ReplaceUserIdentityRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/identities/{Uri.EscapeDataString(target)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Dictionary<string, SocialIdentityEntry>>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task DeleteIdentityAsync(string userId, string target, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/identities/{Uri.EscapeDataString(target)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<User> CreateIdentityAsync(string userId, CreateUserIdentityRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/identities", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/identities.");
    }

    public async Task<IReadOnlyList<UserOrganization>> ListOrganizationsAsync(string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page.HasValue) query.Append($"page={page}&");
        if (pageSize.HasValue) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"users/{Uri.EscapeDataString(userId)}/organizations?{query.ToString().TrimEnd('&')}" : $"users/{Uri.EscapeDataString(userId)}/organizations";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserOrganization[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<IReadOnlyList<UserGrant>> ListGrantsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/grants", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserGrant[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task DeleteGrantAsync(string userId, string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/grants/{Uri.EscapeDataString(applicationId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MfaVerification>> ListMfaVerificationsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/mfa-verifications", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MfaVerification[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<MfaVerificationCreatedResult> CreateMfaVerificationAsync(string userId, CreateMfaVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/mfa-verifications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MfaVerificationCreatedResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/mfa-verifications.");
    }

    public async Task DeleteMfaVerificationAsync(string userId, string verificationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/mfa-verifications/{Uri.EscapeDataString(verificationId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PersonalAccessToken>> ListPersonalAccessTokensAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/personal-access-tokens", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<PersonalAccessToken[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<PersonalAccessToken> CreatePersonalAccessTokenAsync(string userId, CreatePersonalAccessTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/personal-access-tokens", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<PersonalAccessToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/personal-access-tokens.");
    }

    public async Task<PersonalAccessToken> UpdatePersonalAccessTokenAsync(string userId, string tokenName, UpdatePersonalAccessTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"users/{Uri.EscapeDataString(userId)}/personal-access-tokens/{Uri.EscapeDataString(tokenName)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<PersonalAccessToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/personal-access-tokens/{tokenName}.");
    }

    public async Task DeletePersonalAccessTokenAsync(string userId, string tokenName, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/personal-access-tokens/{Uri.EscapeDataString(tokenName)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<UserSsoIdentityDetail> GetSsoIdentityAsync(string userId, string identityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/sso-identities/{Uri.EscapeDataString(identityId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserSsoIdentityDetail>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/sso-identities/{identityId}.");
    }

    public async Task<UserAllIdentities> ListAllIdentitiesAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/all-identities", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserAllIdentities>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/all-identities.");
    }

    public async Task<IReadOnlyList<UserSession>> ListSessionsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/sessions", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserSession[]>(cancellationToken: cancellationToken)
            ?? [];
    }

    public async Task<UserSession> GetSessionAsync(string userId, string sessionId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"users/{Uri.EscapeDataString(userId)}/sessions/{Uri.EscapeDataString(sessionId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserSession>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for users/{userId}/sessions/{sessionId}.");
    }

    public async Task DeleteSessionAsync(string userId, string sessionId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{Uri.EscapeDataString(userId)}/sessions/{Uri.EscapeDataString(sessionId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
