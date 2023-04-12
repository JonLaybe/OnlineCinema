using OnlineCinema.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Utilities
{
    public class ActionsNavigationPage
    {
        public object Parameter { get; set; } // передаваемый параметр
        public MainWindowVM MainWindowVM { get; set; }
        private static ActionsNavigationPage _actionsNavigationPage;

        private ActionsNavigationPage() { }

        public void ChangePage(ViewModelBase viewModel)
        {
            MainWindowVM.ChangePage(viewModel);
        }

        public static ActionsNavigationPage GetActionsNavigationPage() => _actionsNavigationPage == null ? 
            _actionsNavigationPage = new ActionsNavigationPage() : _actionsNavigationPage;
    }
}
