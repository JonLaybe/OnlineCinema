using OnlineCinema.Model;
using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using OnlineCinema.Model.InteractionServer.HandlerCommand;
using OnlineCinema.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OnlineCinema.ViewModel
{
    public class SelectedFilmPageVM : ViewModelBase
    {
        private Post _selectedPost;
        private ActionsNavigationPage _navigationPage;
        private Dispatcher _dispatcher;

        public SelectedFilmPageVM()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _navigationPage = ActionsNavigationPage.GetActionsNavigationPage();
            SelectedPost = (Post)_navigationPage.Parameter;
            HandlerCommand.GetPoster(SelectedPost);
        }

        private void GetPost(Post post)
        {
            Task.Run(() =>
            {
                HandlerCommand.GetPoster(post);
            });
        }

        public Post SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                _selectedPost = value;
                Notify();
            }
        }
        public RelayCommand MainPageBtn
        {
            get
            {
                return new RelayCommand((object parameter) =>
                {
                    _navigationPage.ChangePage(new MainPageVM());
                });
            }
        }
        public RelayCommand PayBtn
        {
            get
            {
                return new RelayCommand((object parameter) =>
                {
                    _navigationPage.ChangePage(new BuyMoviePageVM());
                });
            }
        }
    }
}
