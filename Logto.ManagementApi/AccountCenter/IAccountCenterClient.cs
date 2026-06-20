namespace Logto.ManagementApi.AccountCenter;

public interface IAccountCenterClient
{
    Task<AccountCenterSettings> GetAsync(CancellationToken cancellationToken = default);
    Task<AccountCenterSettings> UpdateAsync(UpdateAccountCenterSettings request, CancellationToken cancellationToken = default);
}
