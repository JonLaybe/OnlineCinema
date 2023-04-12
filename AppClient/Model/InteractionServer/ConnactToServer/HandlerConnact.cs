using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineCinema.Model.InteractionServer.ConnactToServer
{
    public class HandlerConnact
    {
        public ConcurrentBag<ICheckConnact> ListCheckConnacts { get; private set; }
        private Task _mainTask;

        public HandlerConnact()
        {
            ListCheckConnacts = new ConcurrentBag<ICheckConnact>();
        }

        private bool IsSocketConnected(Socket s) // проверяет подключено ли приложение к серверу
        {
            Thread.Sleep(1000);
            return s != null ? (!s.Poll(1000, SelectMode.SelectRead) || s.Available != 0) && s.Connected : false;
        }

        private bool Connact() // подключается к серверу
        {
            try
            {
                ConnactServer.Client = new TcpClient(ConnactServer.Ip, ConnactServer.Port);
                ConnactServer.IsConnact = false;

                return true;
            }
            catch { }
            return false;
        }

        private void TrackingConnact() // следит за подключение к серверу
        {
            while (true)
            {
                if (Connact())
                {
                    while (true)
                    {
                        try
                        {
                            if (IsSocketConnected(ConnactServer.Client.Client) != ConnactServer.IsConnact)
                            {
                                ConnactServer.IsConnact = IsSocketConnected(ConnactServer.Client.Client);

                                if (ConnactServer.IsConnact)
                                {
                                    NotifyConnected();
                                }
                                else
                                {
                                    NotifyDisconnected();
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            NotifyDisconnected();
                            break;
                        }
                    }
                }
            }
        }

        public void Start() // запускает отслеживание подключения
        {
            if (_mainTask == null)
                _mainTask = Task.Run(() => TrackingConnact());
        }

        public void PushInList(ICheckConnact checkConnact) // добавляет в List
        {
            ListCheckConnacts.Add(checkConnact);
        }
        //public void RemoveInList(ICheckConnact checkConnact) => ListCheckConnacts.Remove(checkConnact); // удаляет из List

        private void NotifyConnected() // вызывается при подключении к серверу
        {
            foreach(var item in ListCheckConnacts)
                item.Connected();
        }
        private void NotifyDisconnected() // вызывается при отключении приложения от сервера
        {
            foreach (var item in ListCheckConnacts)
                item.Disconnected();
        }
    }
}
