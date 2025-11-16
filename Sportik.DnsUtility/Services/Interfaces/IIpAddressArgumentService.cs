using System.Net;

namespace Sportik.DnsUtility.Services.Interfaces;

/// <summary>
/// Interface for server parsing command line arguments.
/// </summary>
public interface IIpAddressArgumentService
{
    /// <summary>
    /// Parses the IP address from command line arguments.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="ipAddress">The parsed IP address.</param>
    /// <returns>True if an IP address was successfully parsed; otherwise, false.</returns>
    bool TryParseIpAddress(string[] args, out IPAddress? ipAddress);
}