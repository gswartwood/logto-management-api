using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Applications;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Roles;

public sealed class RolesClient(HttpClient httpClient) : IRolesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<RoleWithStats>> ListAsync(ListRolesRequest? request = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (request?.ExcludeUserId is not null) query.Append($"excludeUserId={Uri.EscapeDataString(request.ExcludeUserId)}&");
        if (request?.ExcludeApplicationId is not null) query.Append($"excludeApplicationId={Uri.EscapeDataString(request.ExcludeApplicationId)}&");
        if (request?.Type is not null) query.Append($"type={request.Type}&");
        if (request?.Page is not null) query.Append($"page={request.Page}&");
        if (request?.PageSize is not null) query.Append($"page_size={request.PageSize}&");
        if (request?.SearchParams is not null)
            foreach (var (key, value) in request.SearchParams)
                query.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}&");
        var url = query.Length > 0 ? $"roles?{query.ToString().TrimEnd('&')}" : "roles";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<RoleWithStats[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles list.");
    }

    public async Task<Role> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Role>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles create.");
    }

    public async Task<Role> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"roles/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Role>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles get.");
    }

    public async Task<Role> UpdateAsync(string id, UpdateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"roles/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Role>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"roles/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Application>> ListApplicationsAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var escaped = Uri.EscapeDataString(roleId);
        var url = query.Length > 0 ? $"roles/{escaped}/applications?{query.ToString().TrimEnd('&')}" : $"roles/{escaped}/applications";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Application[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles applications list.");
    }

    public async Task AssignApplicationsAsync(string roleId, AssignRoleApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"roles/{Uri.EscapeDataString(roleId)}/applications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteApplicationAsync(string roleId, string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"roles/{Uri.EscapeDataString(roleId)}/applications/{Uri.EscapeDataString(applicationId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoleScope>> ListScopesAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var escaped = Uri.EscapeDataString(roleId);
        var url = query.Length > 0 ? $"roles/{escaped}/scopes?{query.ToString().TrimEnd('&')}" : $"roles/{escaped}/scopes";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<RoleScope[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles scopes list.");
    }

    public async Task AssignScopesAsync(string roleId, AssignRoleScopesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"roles/{Uri.EscapeDataString(roleId)}/scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteScopeAsync(string roleId, string scopeId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"roles/{Uri.EscapeDataString(roleId)}/scopes/{Uri.EscapeDataString(scopeId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoleUser>> ListUsersAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var escaped = Uri.EscapeDataString(roleId);
        var url = query.Length > 0 ? $"roles/{escaped}/users?{query.ToString().TrimEnd('&')}" : $"roles/{escaped}/users";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<RoleUser[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for roles users list.");
    }

    public async Task AssignUsersAsync(string roleId, AssignRoleUsersRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"roles/{Uri.EscapeDataString(roleId)}/users", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(string roleId, string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"roles/{Uri.EscapeDataString(roleId)}/users/{Uri.EscapeDataString(userId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
