using System.Net;

namespace Sportik.DnsUtility.Services.Interfaces;

/// <summary>
/// Interface for service managing the Windows hosts file.
/// </summary>
public interface IHostsFileService
{
    /// <summary>
    /// Updates the hosts file with a new IP address for the specified hostname.
    /// Removes any existing entries for the hostname before adding the new entry.
    /// </summary>
    /// <param name="ipAddress">The IP address to map to the hostname.</param>
    /// <param name="hostName">The hostname to update.</param>
    void UpdateHostEntry(IPAddress ipAddress, string hostName);
}