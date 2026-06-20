using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Domains;

[JsonConverter(typeof(JsonStringEnumConverter<DomainStatus>))]
public enum DomainStatus { PendingVerification, PendingSsl, Active, Error }

public record DnsRecord(string Name, string Type, string Value);

public record DomainEntry(
    string Id,
    string Domain,
    DomainStatus Status,
    string? ErrorMessage,
    IReadOnlyList<DnsRecord> DnsRecords,
    double CreatedAt);

public record CreateDomainRequest(string Domain);

public record CleanupDomainsRequest(double StaleDays);

public record CleanupDomainsResult(
    double ScannedCount,
    double DeletedCount,
    double SkippedActiveCount,
    double FailedCount);
