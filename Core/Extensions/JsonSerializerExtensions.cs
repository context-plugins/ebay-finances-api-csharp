using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace EBayFinancesApi.Core.Extensions;

internal static class JsonSerializerExtensions
{
    extension(JsonSerializer)
    {
        public static bool TryDeserialize<T>(JsonElement element, JsonSerializerOptions options,
            [NotNullWhen(true)] out T? result)
        {
            try
            {
                var deserialized = element.Deserialize<T>(options);
                if (deserialized is null)
                {
                    result = default;
                    return false;
                }

                result = deserialized;
                return true;
            }
            catch (JsonException)
            {
                result = default;
                return false;
            }
            catch (NotSupportedException)
            {
                result = default;
                return false;
            }
        }
    }
}