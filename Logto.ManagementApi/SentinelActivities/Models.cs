using System.Text.Json.Serialization;

namespace Logto.ManagementApi.SentinelActivities;

[JsonConverter(typeof(JsonStringEnumConverter<SentinelTargetType>))]
public enum SentinelTargetType { User, App }

public record DeleteSentinelActivitiesRequest(SentinelTargetType TargetType, IReadOnlyList<string> Targets);
