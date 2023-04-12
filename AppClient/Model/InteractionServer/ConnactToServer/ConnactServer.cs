using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.ConnactToServer
{
    public static class ConnactServer
    {
        public static TcpClient Client { get; set; }
        public static string Ip { get; set; }
        public static int Port { get; set; }
        public static bool IsConnact { get; set; }
    }
}
