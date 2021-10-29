using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public static class NetworkUtils
    {
        public static string GetLocalIPAddress()
        {
            IPAddress result = null;
            IPHostEntry iphostentry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] ipv4Address = Array.FindAll(iphostentry.AddressList, add => add.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(add));

            if (ipv4Address.Length > 0)
            {
                return ipv4Address.ToList().FirstOrDefault(x => x.ToString().StartsWith("192.168")).ToString();
            }

            return "192.168.20.1";
        }
    }
}
