using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sportik.DnsUtility.Exceptions;
using Sportik.DnsUtility.Services.Interfaces;

namespace Sportik.DnsUtility.Services.Implementations;

public class HostsFileService(IConfiguration configuration) : IHostsFileService
{
    private readonly string _hostsFilePath = configuration["Dns:HostsFilePath"] ??
                                             throw new ArgumentNullException(nameof(configuration), "Hosts file path configuration is missing.");

    public void UpdateHostEntry(IPAddress ipAddress, string hostName)
    {
        if (ipAddress == null)
        {
            throw new ArgumentException("IP address cannot be null.", nameof(ipAddress));
        }

        if (string.IsNullOrWhiteSpace(hostName))
        {
            throw new ArgumentException("Host name cannot be null or empty.", nameof(hostName));
        }

        try
        {
            List<string> lines = File.Exists(_hostsFilePath)
                ? File.ReadAllLines(_hostsFilePath).ToList()
                : new List<string>();

            lines.RemoveAll(line => ContainsHostName(line, hostName));
            lines.Add($"{ipAddress}\t{hostName}");

            File.WriteAllLines(_hostsFilePath, lines, Encoding.UTF8);
        }
        catch (UnauthorizedAccessException)
        {
            throw new HostsFileAccessException("Administrator privileges are required to modify the hosts file.");
        }
    }

    private static bool ContainsHostName(string line, string hostName)
    {
        int commentIndex = line.IndexOf('#');

        string lineWithoutComment = commentIndex >= 0 ? line.Substring(0, commentIndex) : line;

        lineWithoutComment = lineWithoutComment.Trim();

        if (string.IsNullOrWhiteSpace(lineWithoutComment))
        {
            return false;
        }

        string[] parts = lineWithoutComment.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Equals(hostName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}