using OnlineCinema.Model.InteractionServer.ConnactToServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command
{
    public static class ConnactCommand // команда подключение к серверу
    {
        private static HandlerConnact _handlerConnact;

        static ConnactCommand()
        {
            _handlerConnact = new HandlerConnact();
        }

        public static void PushInList(ICheckConnact checkConnact) => _handlerConnact.PushInList(checkConnact);
        //public static void RemoveInList(ICheckConnact checkConnact) => _handlerConnact.RemoveInList(checkConnact);

        public static void Connact(string ip, int port)
        {
            ConnactServer.Ip = ip;
            ConnactServer.Port = port;

            _handlerConnact.Start();
        }
    }
}
