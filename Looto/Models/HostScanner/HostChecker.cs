using System.Net.NetworkInformation;

namespace Looto.Models.HostScanner
{
    /// <summary>Class for checking host on existance.</summary>
    class HostChecker
    {
        /// <summary>Check host on existance.</summary>
        /// <param name="host">Host to check.</param>
        /// <returns>Bollean value. true - exists. false - don't exists.</returns>
        public static bool CheckHost(HostData host)
        {
            try
            {
                Ping hostCheck = new Ping();
                PingReply reply = hostCheck.Send(host.Host);

                if (reply.Status != IPStatus.Success)
                    return true;
                else return false;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}
