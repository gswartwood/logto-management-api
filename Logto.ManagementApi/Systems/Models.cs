namespace Logto.ManagementApi.Systems;

public record ProtectedAppsConfig(string DefaultDomain);

public record SystemApplicationConfig(ProtectedAppsConfig ProtectedApps);
