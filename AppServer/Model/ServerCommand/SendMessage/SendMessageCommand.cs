using Microsoft.IdentityModel.Tokens;
using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.SendMessage
{
    public class SendMessageCommand // отправка сообщений
    {
        public static void SendMessage(string message, TcpClient client)
        {
            Task.Run(() =>
            {
                if (client.Connected)
                {
                    if (message != "" && message != null)
                    {
                        NetworkStream stream = client.GetStream();

                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine(Base64UrlEncoder.Encode(message));
                        writer.Flush();
                    }
                }
            });
        }
    }
}
