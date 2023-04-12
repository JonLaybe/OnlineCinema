using MainServer.Model.ActionsDb;
using MainServer.Model.ActionsDb.TableDb;
using MainServer.Model.ServerCommand.SendMessage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.GetListGenres
{
    public class GetListGenresCommand : IModelCommand
    {
        public string NameCommand { get; }

        public string SkinCommnad { get; }

        public GetListGenresCommand()
        {
            NameCommand = "GetListGenres";
            SkinCommnad = "GetListGenres";
        }

        private class ContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);

                if (property.DeclaringType == typeof(Genre) && property.PropertyName == "Films")
                {
                    property.ShouldSerialize = instance => false;
                }

                return property;
            }
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
                        { "Value", JsonConvert.SerializeObject(ConnactedDb.ContextModelDb.Genres, new JsonSerializerSettings
                        {
                            ContractResolver = new ContractResolver()
                        }) },
                    };

                    SendMessageCommand.SendMessage(JsonConvert.SerializeObject(answer), (TcpClient)client);
                }, (string message) =>
                {
                    return message == $"<{SkinCommnad}>" ? true : false;
                });
            }
        }

        public void Callback(string message, object client)
        {
            Command.Handler(message, client);
        }
    }
}
