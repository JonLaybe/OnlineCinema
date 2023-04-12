using MainServer.Model.ServerCommand.SendMessage;
using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace MainServer.Model.ServerCommand.None
{
    public class NoneCommand : IModelCommand // нераспознанная команда
    {
        public string NameCommand { get; }

        public string SkinCommnad { get; }

        private string _errorMessage;

        public NoneCommand()
        {
            NameCommand = "NoneCommand";
            SkinCommnad = "NoneCommand";
            _errorMessage = "invalid command syntax";
        }

        public ICommand Command
        {
            get
            {
                return new ModelCommand((string message, object client) =>
                {
                    TcpClient tcpClient = (TcpClient)client;

                    SendMessageCommand.SendMessage(JsonConvert.SerializeObject(new Dictionary<string, string>()
                    {
                        { "NameCommand", $"{SkinCommnad}" },
                        { "Error", _errorMessage },
                    }), tcpClient);
                }, (string message) => true);
            }
        }

        public void Callback(string message, object client)
        {
            Command.Handler(message, client);
        }
    }
}
