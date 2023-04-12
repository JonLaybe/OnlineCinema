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

namespace MainServer.Model.ServerCommand.GetListPosters
{
    public class GetListPostersCommand : IModelCommand
    {
        public string NameCommand { get; }

        public string SkinCommnad { get; }

        public GetListPostersCommand()
        {
            NameCommand = "GetListPosters";
            SkinCommnad = "GetListPosters";
        }

        /*private static byte[] ConvertImageToByte(string path)
        {
            return File.ReadAllBytes(path);
        }*/

        private List<Poster> ConvertList(IEnumerable<AdapterPoster> list) //TODO
        {
            List<Poster> tempListPosters = new List<Poster>();

            foreach(var item in list)
            {
                tempListPosters.Add(item);
            }

            return tempListPosters;
        }

        public ICommand Command
        {
            get
            {
                return new ModelCommand((string message, object client) =>
                {
                    List<Poster> list_films;
                    string[] array_sub = message.Split(' ');

                    int num_from = Convert.ToInt32(array_sub[1]);
                    int num_to = Convert.ToInt32(array_sub[2]);

                    if(num_to > 0 && num_from > 0 && num_from <= ConnactedDb.CountFilms)
                    {
                        var films = ConnactedDb.ContextModelDb.Films.Include(c => c.Genres).ToList();
                        list_films = ConvertList(from item in films where item.Id >= num_from && item.Id <= (num_from + num_to) - 1 select new AdapterPoster(item));
                    }
                    else
                    {
                        list_films = new List<Poster>();
                    }
                    SendMessageCommand.SendMessage(JsonConvert.SerializeObject(new Dictionary<string, string>()
                    {
                        { "NameCommand", NameCommand },
                        { "Value", JsonConvert.SerializeObject(list_films) },
                    }), (TcpClient)client);
                }, (string message) =>
                {
                    return Regex.IsMatch(message, $@"^<{SkinCommnad}>\s\d\s\d$");
                });
            }
        }

        public void Callback(string message, object client)
        {
            Command.Handler(message, client);
        }
    }
}
