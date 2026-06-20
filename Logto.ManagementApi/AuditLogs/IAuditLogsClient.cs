namespace Logto.ManagementApi.AuditLogs;

public interface IAuditLogsClient
{
    Task<IEnumerable<AuditLog>> ListAsync(AuditLogsListOptions? options = null, CancellationToken cancellationToken = default);
    Task<AuditLog> GetAsync(string id, CancellationToken cancellationToken = default);
}
