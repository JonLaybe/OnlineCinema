using MainServer.Model.ActionsDb;
using MainServer.Model.ServerCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerModel;

namespace MainServer.Model.TcpServer
{
    public class TcpServer : INotifyAccept
    {
        private Server _server;
        private HandlerCommand _handlerCommand;

        public TcpServer()
        {
            _server = new Server("Server", 1500);
            _server.Property = this;
            _server.IsFirstPost = false;

            _handlerCommand = new HandlerCommand();
            ConnactedDb.Connact();

            _server.Start();
        }

        public void ConnactClient(TcpClient client)
        {
            Console.WriteLine("Connact");
        }

        public void DisconnectClient(TcpClient client)
        {
            Console.WriteLine("Disconnect");
        }

        public void FirstPost(string message, TcpClient client)
        {
            Console.WriteLine($"First message: {message}");
        }

        public void GetMessage(string message, TcpClient client)
        {
            Console.WriteLine($"Message: {message}");
            _handlerCommand.Handler(message, client);
        }
    }
}
