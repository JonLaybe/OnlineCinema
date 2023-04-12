using OnlineCinema.Model;
using OnlineCinema.Model.InteractionServer.Command;
using OnlineCinema.Model.InteractionServer.ConnactToServer;
using OnlineCinema.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineCinema.ViewModel
{
    public class MainWindowVM : ViewModelBase, ICheckConnact, INavigationPage
    {
        private string _namePage;
        private object _currentView;
        private ActionsNavigationPage _actionsNavigationPage;
        private object _vm;

        public MainWindowVM()
        {
            _actionsNavigationPage = ActionsNavigationPage.GetActionsNavigationPage();
            _actionsNavigationPage.MainWindowVM = this;
            ConnactCommand.PushInList(this);

            ConnactCommand.Connact("127.0.0.1", 1500);

            ChangePage(new MainPageVM());
        }

        public void Connected()
        {
            //MessageBox.Show("Connact");
        }

        public void Disconnected()
        {
            MessageBox.Show("Disconnected");
        }

        public void ChangePage(ViewModelBase viewModel) // изменение страниц
        {
            if (viewModel.NameViewModel == nameof(MainPageVM))
            {
                if (_vm == null)
                    _vm = viewModel;
                CurrentView = _vm;
            }
            else
            {
                CurrentView = viewModel;
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                Notify();
            }
        }

        public string NamePage
        {
            get { return _namePage; }
            set
            {
                _namePage = value;
                Notify("NamePage");
            }
        }
    }
}
