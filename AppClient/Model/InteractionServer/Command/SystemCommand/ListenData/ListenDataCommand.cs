using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.SystemCommand.ListenData
{
    public class ListenDataCommand : IServerCommnad
    {
        private static ListenDataCommand _listenDataCommand;
        public string Name { get; set; }
        public string Command { get; set; }

        private ListenDataCommand()
        {
            Name = "ListenData";
        }

        public Task<string?> Handler(TcpClient client)
        {
            return Task.Run(() =>
            {
                try
                {
                    NetworkStream stream;
                    stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string message = reader.ReadLine();
                    return Base64UrlEncoder.Decode(message);
                }
                catch
                {
                    client.Close();
                    return null;
                }
            });
        }

        public static ListenDataCommand GetListenDataCommand() => _listenDataCommand == null ? _listenDataCommand = new ListenDataCommand() : _listenDataCommand;
    }
}
