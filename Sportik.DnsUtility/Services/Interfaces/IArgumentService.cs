using System.Net;

namespace Sportik.DnsUtility.Services.Interfaces;

/// <summary>
/// Interface for parsing command line arguments.
/// </summary>
public interface IArgumentService
{
    /// <summary>
    /// Parses the IP address from command line arguments.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="ipAddress">The parsed IP address.</param>
    /// <returns>True if an IP address was successfully parsed; otherwise, false.</returns>
    bool TryParseIpAddress(string[] args, out IPAddress? ipAddress);

    /// <summary>
    /// Parses the host name from command line arguments.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="hostName">The parsed host name.</param>
    /// <returns>True if a host name was successfully parsed; otherwise, false.</returns>
    bool TryParseHostName(string[] args, out string? hostName);
}