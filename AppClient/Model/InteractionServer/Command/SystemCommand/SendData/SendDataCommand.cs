using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.SystemCommand.SendData
{
    public class SendDataCommand : ICreateServerCommand // отправка данных
    {
        private static SendDataCommand _sendDataCommand;
        public string Name { get; set; }
        public string Command { get; set; }

        private SendDataCommand()
        {
            Name = "SendData";
        }

        public void Handler(string message, TcpClient client)
        {
            if (message != null && message != "")
            {
                Task.Run(() =>
                {
                    TcpClient tcpClient = (TcpClient)client;
                    if (tcpClient.Connected)
                    {
                        if (message != "" && message != null)
                        {
                            try
                            {
                                NetworkStream stream = tcpClient.GetStream();

                                StreamWriter writer = new StreamWriter(stream);
                                writer.WriteLine(message);
                                writer.Flush();
                            }
                            catch (Exception ex) { }
                        }
                    }
                });
            }
        }

        public static SendDataCommand GetSendDataCommand() => _sendDataCommand == null ? _sendDataCommand = new SendDataCommand() : _sendDataCommand;

        public string GetCommand(object[] parameter)
        {
            throw new NotImplementedException();
        }
    }
}
