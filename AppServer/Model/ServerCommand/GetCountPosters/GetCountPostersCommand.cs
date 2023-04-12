using MainServer.Model.ActionsDb;
using MainServer.Model.ServerCommand.SendMessage;
using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MainServer.Model.ServerCommand.GetCountPosters
{
    public class GetCountPostersCommand : IModelCommand //количество кинокартин
    {
        public string NameCommand { get; }

        public string SkinCommnad { get; }

        public GetCountPostersCommand()
        {
            NameCommand = "GetCountPosters";
            SkinCommnad = "GetCountPosters";
        }

        public ICommand Command
        {
            get
            {
                return new ModelCommand((string message, object client) =>
                {
                    Dictionary<string, string> answer = new Dictionary<string, string>()
                    {
                        { "NameCommand", $"{NameCommand}" },
                        { "Value", ConnactedDb.ContextModelDb.Films.Count().ToString() },
                    };

                    SendMessageCommand.SendMessage(JsonConvert.SerializeObject(answer), (TcpClient)client);
                }, (string message) =>
                {
                    return message == $"<{SkinCommnad}>";
                });
            }
        }

        public void Callback(string message, object client)
        {
            Command.Handler(message, client);
        }
    }
}
