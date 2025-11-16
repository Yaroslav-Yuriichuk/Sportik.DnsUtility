using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Sportik.DnsUtility.Extensions;

public static class ConfigurationExtension
{
    public static IConfigurationBuilder AddEmbeddedJsonFile(
        this IConfigurationBuilder builder,
        string resourceName,
        Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        using Stream? sourceStream = assembly.GetManifestResourceStream(resourceName);

        if (sourceStream == null)
        {
            throw new FileNotFoundException(
                $"Embedded resource '{resourceName}' not found in assembly '{assembly.FullName}'. " +
                $"Available resources: {string.Join(", ", assembly.GetManifestResourceNames())}");
        }

        using MemoryStream tempStream = new();

        sourceStream.CopyTo(tempStream);
        byte[] buffer = tempStream.ToArray();

        MemoryStream memoryStream = new(buffer, writable: false);

        return builder.AddJsonStream(memoryStream);
    }
}

