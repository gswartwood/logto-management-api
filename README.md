# Logto.ManagementApi

An unofficial .NET client library for the [Logto Management API](https://docs.logto.io/docs/references/management-api/). Handles machine-to-machine (M2M) authentication automatically, including token acquisition and refresh. This library is for interacting with Logto's management API. It provides no support in providing client/server side authentication/authorization of users or machine-to-machine (MTM) tokens. If you are looking for that, please refer to [Logto C#](https://github.com/logto-io/csharp) page.

## Features

- Covers the full Logto Management API surface (users, organizations, applications, roles, connectors, sign-in experience, and more)
- M2M client credentials flow handled transparently — no manual token management
- Supports both **Logto Cloud** and **self-hosted OSS** instances
- Designed for ASP.NET Core dependency injection

## Installation

```
dotnet add package Logto.ManagementApi
```

## Configuration

You will need a **Machine-to-Machine application** in your Logto tenant with the Management API resource assigned. The client ID and secret come from that application.

### Logto Cloud

```csharp
builder.Services.AddLogtoMgmtApiClient(options =>
{
    options.TenantId = "your-tenant-id";
    options.ManagementApiClientId = "your-m2m-client-id";
    options.ManagementClientSecret = "your-m2m-client-secret";
});
```

### Self-hosted (Logto OSS)

```csharp
builder.Services.AddLogtoMgmtApiClient(options =>
{
    options.BaseUrl = "https://auth.example.com";
    options.ManagementApiClientId = "your-m2m-client-id";
    options.ManagementClientSecret = "your-m2m-client-secret";
});
```

## Usage

Inject `ILogtoMgmtApiClient` wherever you need it:

```csharp
public class UserService(ILogtoMgmtApiClient logto)
{
    public async Task<string[]> GetUserEmailsAsync(CancellationToken ct)
    {
        var users = await logto.Users.GetUsersAsync(cancellationToken: ct);
        return users.Select(u => u.PrimaryEmail).OfType<string>().ToArray();
    }
}
```

Available endpoint groups on `ILogtoMgmtApiClient`:

| Property | Description |
|---|---|
| `Users` | User management |
| `Organizations` | Organization management |
| `Applications` | Application management |
| `Roles` / `OrganizationRoles` | Role management |
| `Resources` | API resource management |
| `Connectors` | Social/enterprise connector management |
| `SignInExperience` | Sign-in experience configuration |
| `AuditLogs` | Audit log access |
| `Hooks` | Webhook management |
| `Domains` | Custom domain management |
| `Dashboard` | Usage statistics |
| ... | And more |

## License

Distributed under the MIT License.
See the [LICENSE](./LICENSE) file for more information.
