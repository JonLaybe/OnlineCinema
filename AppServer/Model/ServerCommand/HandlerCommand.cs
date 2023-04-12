using MainServer.Model.ServerCommand.GetCountPosters;
using MainServer.Model.ServerCommand.GetListGenres;
using MainServer.Model.ServerCommand.GetListPosters;
using MainServer.Model.ServerCommand.GetPosterById;
using MainServer.Model.ServerCommand.None;
using ServerCommandModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand
{
    public class HandlerCommand // обработчик команд
    {
        private CheckCommand _checkCommand; // класс для обработки комманд

        private GetCountPostersCommand _getCountPostersCommand; // получает количество кинокортив в таблице "Film"
        private GetListPostersCommand _getListPostersCommand;
        private GetListGenresCommand _getListGenresCommand;
        private GetPosterByIdCommnad _getPosterByIdCommnad;
        private NoneCommand _noneCommand; // комнда для оповещение клиента об ошибке

        public HandlerCommand()
        {
            _getCountPostersCommand = new GetCountPostersCommand();
            _getListPostersCommand = new GetListPostersCommand();
            _getPosterByIdCommnad = new GetPosterByIdCommnad();
            _getListGenresCommand = new GetListGenresCommand();
            _noneCommand = new NoneCommand();

            _checkCommand = new CheckCommand(new List<ServerCommandModel.ModelCommands.ICommand>()
            {
                _getCountPostersCommand.Command,
                _getListPostersCommand.Command,
                _getPosterByIdCommnad.Commnad,
                _getListGenresCommand.Command,
                _noneCommand.Command,
            });
        }

        public void Handler(string message, object client)
        {
            _checkCommand.Check(message, client);
        }
    }
}
