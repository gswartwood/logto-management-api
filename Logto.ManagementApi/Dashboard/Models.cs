namespace Logto.ManagementApi.Dashboard;

public record TotalUserCountResult(double TotalUserCount);

public record UserCountPeriod(double Count, double Delta);

public record NewUserCounts(UserCountPeriod Today, UserCountPeriod Last7Days);

public record DauPoint(string Date, double Count);

public record ActiveUserCounts(
    IReadOnlyList<DauPoint> DauCurve,
    UserCountPeriod Dau,
    UserCountPeriod Wau,
    UserCountPeriod Mau);
