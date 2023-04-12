using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using OnlineCinema.Model.InteractionServer.Command;
using OnlineCinema.Model.InteractionServer.Command.GetPosters;
using OnlineCinema.Model.InteractionServer.Command.SystemCommand.ListenData;
using OnlineCinema.Model.InteractionServer.Command.SystemCommand.SendData;
using OnlineCinema.Model.InteractionServer.ConnactToServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using OnlineCinema.Model.InteractionServer.Command.GetCountPosters;
using OnlineCinema.Model.InteractionServer.Command.GetPosterById;
using OnlineCinema.Model.InteractionServer.Command.GetGenres;
using System.Windows.Documents;

namespace OnlineCinema.Model.InteractionServer.HandlerCommand
{
    //TODO сделать одну шаблон функцию для всех команд
    public static class HandlerCommand // обработчик команд
    {
        private static SendDataCommand _sendDataCommand;
        private static ListenDataCommand _listenDataCommand;
        private static GetPostersCommand _getPostersCommand;
        private static GetCountPostersCommnad _getCountPostersCommnad;
        private static GetGenresCommand _getGenresCommand;
        private static GetPosterByIdCommnad _getPosterCommnad;

        static HandlerCommand()
        {
            _sendDataCommand = SendDataCommand.GetSendDataCommand();
            _listenDataCommand = ListenDataCommand.GetListenDataCommand();
            _getPostersCommand = new GetPostersCommand();
            _getCountPostersCommnad = new GetCountPostersCommnad();
            _getPosterCommnad = new GetPosterByIdCommnad();
            _getGenresCommand = new GetGenresCommand();
        }

        private static void SendMessage(string message) // отпрвка данных на сервер
        {
            _sendDataCommand.Handler(message, ConnactServer.Client);
        }
        private static string? ListenServer() // прослушивает сервер и возращает ответ
        {
            Task<string?> task_listen = _listenDataCommand.Handler(ConnactServer.Client);

            task_listen.Wait();

            return task_listen.Result;
        }

        public static List<CoverPost>? GetPosts(int first, int last) // команда получить облошек кинокартин
        {
            try
            {
                if (ConnactServer.IsConnact)
                {
                    SendMessage(_getPostersCommand.GetCommand(new object[] { first, last }));
                    string? result_listen = ListenServer();

                    if (result_listen != null || result_listen != "")
                    {
                        Dictionary<string, string>? keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(result_listen);
                        return keyValuePairs != null && keyValuePairs.ContainsKey("Value") ? JsonConvert.DeserializeObject<List<CoverPost>>(keyValuePairs["Value"]) : new List<CoverPost>();
                    }
                }
                throw new Exception("No connection to server");
            }
            catch (Exception ex) { }
            return new List<CoverPost>();
        }
        public static int GetCountPosters() // команда получение числа постеров
        {
            try
            {
                if (ConnactServer.IsConnact)
                {
                    SendMessage(_getCountPostersCommnad.GetCommand(null));

                    string result_listen = ListenServer();

                    if (result_listen != null || result_listen != "")
                    {
                        Dictionary<string, string>? keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(result_listen);
                        return keyValuePairs != null && keyValuePairs.ContainsKey("Value") ? Convert.ToInt32(keyValuePairs["Value"]) : 0;
                    }
                }
            }catch (Exception ex) { }

            return 0;
        }
        public static void GetPoster(Post post) // получение постера по id
        {
            try
            {
                if (ConnactServer.IsConnact)
                {
                    SendMessage(_getPosterCommnad.GetCommand(new object[] { post.Id }));

                    string result_listen = ListenServer();

                    if (result_listen != null || result_listen != "")
                    {
                        Dictionary<string, string>? keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(result_listen);
                        if (keyValuePairs != null && keyValuePairs.ContainsKey("Value"))
                        {
                            Post? temp_post = JsonConvert.DeserializeObject<Post>(keyValuePairs["Value"]);
                            if (temp_post != null)
                            {
                                post.Year = temp_post.Year;
                                post.Timing = temp_post.Timing;
                                post.Price = temp_post.Price;
                                post.Description = temp_post.Description;
                            }
                        }

                    }
                }
            }
            catch (Exception ex) { }
        }
        public static List<Genre> GetGenres() // команда для получения жанров
        {
            try
            {
                if (ConnactServer.IsConnact)
                {
                    SendMessage(_getGenresCommand.GetCommand(null));
                    string? result_listen = ListenServer();

                    if (result_listen != null || result_listen != "")
                    {
                        Dictionary<string, string>? keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(result_listen);
                        return keyValuePairs != null && keyValuePairs.ContainsKey("Value") ? JsonConvert.DeserializeObject<List<Genre>>(keyValuePairs["Value"]) : new List<Genre>();
                    }
                }
                throw new Exception("No connection to server");
            }
            catch (Exception ex) { }
            return new List<Genre>();
        }
    }
}
