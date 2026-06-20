namespace Logto.ManagementApi.Secrets;

public interface ISecretsClient
{
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
