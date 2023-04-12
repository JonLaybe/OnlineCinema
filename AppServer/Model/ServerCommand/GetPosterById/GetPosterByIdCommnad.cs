using MainServer.Model.ActionsDb;
using MainServer.Model.ActionsDb.TableDb;
using MainServer.Model.ServerCommand.SendMessage;
using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace MainServer.Model.ServerCommand.GetPosterById
{
    public class GetPosterByIdCommnad : IModelCommand // получение дополнительных сведений о кинокартине
    {
        public string NameCommand { get; }

        public string SkinCommnad { get; }

        public GetPosterByIdCommnad()
        {
            NameCommand = "GetPosterById";
            SkinCommnad = "GetPosterById";
        }

        public ModelCommand Commnad
        {
            get
            {
                return new ModelCommand((string message, object client) =>
                {
                    string[] array_sub = message.Split(' ');
                    SuplementPoster temp_film = new SuplementPoster();
                    int index = Convert.ToInt32(array_sub[1]); // id film

                    if (index <= ConnactedDb.CountFilms)
                    {
                        temp_film = (from item in ConnactedDb.ContextModelDb.Films where item.Id == index select new AdapterSuplementPoster(item)).First();
                    }
                    else
                        temp_film = new SuplementPoster();

                    SendMessageCommand.SendMessage(JsonConvert.SerializeObject(new Dictionary<string, string>()
                    {
                        { "NameCommand", NameCommand },
                        { "Value", JsonConvert.SerializeObject(temp_film) },
                    }), (TcpClient)client);
                }, (string message) =>
                {
                    return Regex.IsMatch(message, $@"^<{SkinCommnad}>\s\d$");
                });
            }
        }

        public void Callback(string message, object client)
        {
            Commnad.Handler(message, client);
        }
    }
}
