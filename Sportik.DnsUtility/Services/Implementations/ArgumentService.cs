using System.Net;
using Microsoft.Extensions.Configuration;
using Sportik.DnsUtility.Services.Interfaces;

namespace Sportik.DnsUtility.Services.Implementations;

public class ArgumentService(IConfiguration configuration) : IArgumentService
{
    private const string AddressArgument = "--addr";
    private const string HostNameArgument = "--host";

    private readonly string _defaultIpAddress = configuration["Dns:DefaultIpAddress"] ??
                                                throw new ArgumentNullException(nameof(configuration), "Default IP address configuration is missing.");

    private readonly string _defaultHostName = configuration["Dns:HostName"] ??
                                               throw new ArgumentNullException(nameof(configuration), "Default host name configuration is missing.");

    public bool TryParseIpAddress(string[] args, out IPAddress? ipAddress)
    {
        string ipString = _defaultIpAddress;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == AddressArgument && i + 1 < args.Length)
            {
                ipString = args[i + 1].Trim('"', '\'');
                break;
            }
        }

        return IPAddress.TryParse(ipString, out ipAddress);
    }

    public bool TryParseHostName(string[] args, out string? hostName)
    {
        hostName = _defaultHostName;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == HostNameArgument && i + 1 < args.Length)
            {
                hostName = args[i + 1].Trim('"', '\'');
                break;
            }
        }

        return !string.IsNullOrWhiteSpace(hostName);
    }
}