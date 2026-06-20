namespace Logto.ManagementApi.Users;

public interface IUsersClient
{
    Task<IReadOnlyList<User>> ListAsync(int? page = null, int? pageSize = null, string? search = null, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<User> GetAsync(string userId, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string userId, CancellationToken cancellationToken = default);

    Task<Dictionary<string, object>> GetCustomDataAsync(string userId, CancellationToken cancellationToken = default);
    Task<Dictionary<string, object>> UpdateCustomDataAsync(string userId, UpdateUserCustomDataRequest request, CancellationToken cancellationToken = default);

    Task<UserLogtoConfigs> GetLogtoConfigsAsync(string userId, CancellationToken cancellationToken = default);
    Task<UserLogtoConfigs> UpdateLogtoConfigsAsync(string userId, UpdateUserLogtoConfigsRequest request, CancellationToken cancellationToken = default);

    Task<UserProfile> UpdateProfileAsync(string userId, UpdateUserProfileRequest request, CancellationToken cancellationToken = default);

    Task UpdatePasswordAsync(string userId, UpdateUserPasswordRequest request, CancellationToken cancellationToken = default);
    Task UpdatePasswordExpirationAsync(string userId, UpdateUserPasswordExpirationRequest request, CancellationToken cancellationToken = default);
    Task VerifyPasswordAsync(string userId, VerifyUserPasswordRequest request, CancellationToken cancellationToken = default);
    Task<UserHasPasswordResult> GetHasPasswordAsync(string userId, CancellationToken cancellationToken = default);

    Task<User> UpdateIsSuspendedAsync(string userId, UpdateUserIsSuspendedRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserRole>> ListRolesAsync(string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignRolesAsync(string userId, AssignUserRolesRequest request, CancellationToken cancellationToken = default);
    Task ReplaceRolesAsync(string userId, AssignUserRolesRequest request, CancellationToken cancellationToken = default);
    Task DeleteRoleAsync(string userId, string roleId, CancellationToken cancellationToken = default);

    Task<UserIdentityDetail> GetIdentityAsync(string userId, string target, CancellationToken cancellationToken = default);
    Task<Dictionary<string, SocialIdentityEntry>> ReplaceIdentityAsync(string userId, string target, ReplaceUserIdentityRequest request, CancellationToken cancellationToken = default);
    Task DeleteIdentityAsync(string userId, string target, CancellationToken cancellationToken = default);
    Task<User> CreateIdentityAsync(string userId, CreateUserIdentityRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserOrganization>> ListOrganizationsAsync(string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserGrant>> ListGrantsAsync(string userId, CancellationToken cancellationToken = default);
    Task DeleteGrantAsync(string userId, string applicationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MfaVerification>> ListMfaVerificationsAsync(string userId, CancellationToken cancellationToken = default);
    Task<MfaVerificationCreatedResult> CreateMfaVerificationAsync(string userId, CreateMfaVerificationRequest request, CancellationToken cancellationToken = default);
    Task DeleteMfaVerificationAsync(string userId, string verificationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PersonalAccessToken>> ListPersonalAccessTokensAsync(string userId, CancellationToken cancellationToken = default);
    Task<PersonalAccessToken> CreatePersonalAccessTokenAsync(string userId, CreatePersonalAccessTokenRequest request, CancellationToken cancellationToken = default);
    Task<PersonalAccessToken> UpdatePersonalAccessTokenAsync(string userId, string tokenName, UpdatePersonalAccessTokenRequest request, CancellationToken cancellationToken = default);
    Task DeletePersonalAccessTokenAsync(string userId, string tokenName, CancellationToken cancellationToken = default);

    Task<UserSsoIdentityDetail> GetSsoIdentityAsync(string userId, string identityId, CancellationToken cancellationToken = default);

    Task<UserAllIdentities> ListAllIdentitiesAsync(string userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserSession>> ListSessionsAsync(string userId, CancellationToken cancellationToken = default);
    Task<UserSession> GetSessionAsync(string userId, string sessionId, CancellationToken cancellationToken = default);
    Task DeleteSessionAsync(string userId, string sessionId, CancellationToken cancellationToken = default);
}
