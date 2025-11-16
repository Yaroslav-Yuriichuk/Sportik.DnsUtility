namespace Sportik.DnsUtility.Exceptions;

/// <summary>
/// Exception thrown when access to the hosts file is denied, typically due to insufficient privileges.
/// </summary>
public class HostsFileAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HostsFileAccessException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public HostsFileAccessException(string message) : base(message)
    {
    }
}