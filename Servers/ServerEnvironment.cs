using System;
using System.Text.Json.Serialization;
using EBayFinancesApi.Core.Enum;

namespace EBayFinancesApi.Servers;

[JsonConverter(typeof(StringEnumConverter<ServerEnvironment>))]
public record ServerEnvironment : StringEnum<ServerEnvironment>
{
    /// <summary>
    /// Production
    /// </summary>
    public static readonly ServerEnvironment Production = new("production");

    private ServerEnvironment(string value) : base(value)
    {
    }

    internal T Match<T>(Func<T> onProduction) =>
        this switch
        {
            _ when this == Production => onProduction(),
            _ => throw new ArgumentOutOfRangeException(nameof(ServerEnvironment),
                this,
                $"Unknown {nameof(ServerEnvironment)} value.")
        };

    public static ServerEnvironment Default() => Production;
}
