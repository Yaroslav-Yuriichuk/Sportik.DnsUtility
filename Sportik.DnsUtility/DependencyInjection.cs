using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportik.DnsUtility.Services.Implementations;
using Sportik.DnsUtility.Services.Interfaces;

namespace Sportik.DnsUtility;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IIpAddressArgumentService, IpAddressArgumentService>();
        services.AddSingleton<IHostsFileService, HostsFileService>();

        return services;
    }
}