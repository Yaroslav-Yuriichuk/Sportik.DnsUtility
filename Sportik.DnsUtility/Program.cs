using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportik.DnsUtility;
using Sportik.DnsUtility.Exceptions;
using Sportik.DnsUtility.Extensions;
using Sportik.DnsUtility.Services.Interfaces;

Assembly assembly = Assembly.GetExecutingAssembly();

string assemblyName = assembly.GetName().Name ?? "Sportik.DnsUtility";
string resourceName = $"{assemblyName}.appsettings.json";

IConfiguration configuration = new ConfigurationBuilder()
    .AddEmbeddedJsonFile(resourceName, assembly)
    .Build();

IServiceCollection services = new ServiceCollection();

services.AddSingleton(configuration);
services.AddApplication(configuration);

ServiceProvider serviceProvider = services.BuildServiceProvider();

try
{
    IArgumentService argumentService = serviceProvider.GetRequiredService<IArgumentService>();
    IHostsFileService hostsFileService = serviceProvider.GetRequiredService<IHostsFileService>();

    if (!argumentService.TryParseIpAddress(args, out IPAddress? ipAddress))
    {
        Console.WriteLine("Error: Unable to parse IP address.");
        return 0;
    }

    if (!argumentService.TryParseHostName(args, out string? hostName))
    {
        Console.WriteLine("Error: Unable to parse host name.");
        return 0;
    }

    hostsFileService.UpdateHostEntry(ipAddress!, hostName!);

    Console.WriteLine($"Successfully updated Dns configuration for {ipAddress}.");
    Console.WriteLine("Press any key to exit.");
}
catch (HostsFileAccessException)
{
    Console.WriteLine("Please run this program as Administrator.");
    Console.WriteLine("Press any key to exit.");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
    Console.WriteLine("Press any key to exit.");
}

Console.ReadKey();

return 0;