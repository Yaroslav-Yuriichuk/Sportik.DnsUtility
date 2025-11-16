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
    IIpAddressArgumentService ipAddressArgumentService = serviceProvider.GetRequiredService<IIpAddressArgumentService>();
    IHostsFileService hostsFileService = serviceProvider.GetRequiredService<IHostsFileService>();

    if (!ipAddressArgumentService.TryParseIpAddress(args, out IPAddress? ipAddress))
    {
        Console.WriteLine("Error: Unable to parse IP address.");
        return 0;
    }

    string hostName = configuration["Dns:HostName"] ?? throw new ArgumentNullException(nameof(configuration), "Host name configuration is missing.");
    hostsFileService.UpdateHostEntry(ipAddress!, hostName);

    Console.WriteLine($"Successfully updated Dns configuration for {ipAddress}.");
}
catch (HostsFileAccessException)
{
    Console.WriteLine("Please run this program as Administrator.");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

Console.ReadKey();

return 0;