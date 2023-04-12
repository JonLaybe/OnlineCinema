using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32;
using OnlineCinema.Model;
using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using OnlineCinema.Model.InteractionServer.Command;
using OnlineCinema.Model.InteractionServer.ConnactToServer;
using OnlineCinema.Model.InteractionServer.HandlerCommand;
using OnlineCinema.Model.PopupGenres;
using OnlineCinema.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OnlineCinema.ViewModel
{
    public class MainPageVM : ViewModelBase, ICheckConnact, ICheckListGenres
    {
        private Dispatcher _dispatcher;
        private ActionsNavigationPage _navigationPages; // навигация страниц
        private List<Post> _listPosters = new List<Post>();
        private bool _popupIsOpen;
        private ObservableCollection<Post> _viewListPosters; // отображаемый список кинокартин в listbox
        private ObservableCollection<PopupGenre> _viewListGenres; // отображаемый список жанров в listbox
        private string _foundTextBlock;
        private Post _selectedPost; // выбранная кинокартина

        public MainPageVM()
        {
            NameViewModel = nameof(MainPageVM);
            _navigationPages = ActionsNavigationPage.GetActionsNavigationPage();
            ConnactCommand.PushInList(this);

            _viewListPosters = new ObservableCollection<Post>();
            _viewListGenres = new ObservableCollection<PopupGenre>();

            _dispatcher = Dispatcher.CurrentDispatcher;

            _popupIsOpen = false;
        }

        private void GetPosters(int num_from, int num_to) // получение кинокартин
        {
            Task.Run(() =>
            {
                List<CoverPost>? list_posters = HandlerCommand.GetPosts(num_from, num_to);

                foreach (var item in list_posters)
                {
                    _dispatcher.Invoke(() =>
                    {
                        AdapterPoster temp_poster = new AdapterPoster(item);
                        _listPosters.Add(temp_poster);
                        ViewListPosters.Add(temp_poster);
                    });
                }
            });
        }
        private List<PopupGenre> Adapter(List<Genre> listGenres)
        {
            List<PopupGenre> listPopupGenres = new List<PopupGenre>();

            foreach(var item in listGenres)
            {
                listPopupGenres.Add(new PopupGenre()
                {
                    Id = item.Id,
                    Name = item.Name,
                    CheckListGenres = this,
                });
            }

            return listPopupGenres;
        }
        private void GetGenres() // получение кинокартин
        {
            Task.Run(() =>
            {
                ViewListGenres = new ObservableCollection<PopupGenre>(Adapter(HandlerCommand.GetGenres()));
            });
        }

        public void Connected() // метод вызывается есть приложение подключислось к серверу
        {
            Task.Run(() =>
            {
                int count_posters = HandlerCommand.GetCountPosters();
                if (_listPosters.Count() < count_posters)
                {
                    while (_listPosters.Count() < count_posters)
                    {
                        GetPosters(_listPosters.Count() + 1, 1);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    ViewListPosters = new ObservableCollection<Post>(_listPosters);
                }

                if (ViewListGenres.Count() == 0)
                {
                    GetGenres();
                }
            });
        }

        public void Disconnected() // метод вызывается когда приложение отключилось от сервера
        {
            //throw new NotImplementedException();
        }

        public void UpdateListGenres()
        {
            List<string> tempList = new List<string>();

            foreach (var item in ViewListGenres)
                if (item.IsChecked)
                    tempList.Add(item.Name);
            if (tempList.Count() > 0)
            {
                ViewListPosters.Clear();
                foreach (var itemPost in _listPosters)
                {
                    bool a = true;
                    foreach (var itemGenres in tempList)
                    {
                        if (a)
                        {
                            foreach (var item in itemPost.Genres)
                            {
                                if (itemGenres == item)
                                {
                                    ViewListPosters.Add(itemPost);
                                    a = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ViewListPosters.Clear();
                foreach(var item in _listPosters) // вынести в отдельную функцию
                {
                    ViewListPosters.Add(item);
                }
            }
        }

        public string FoundTextBlock
        {
            get { return _foundTextBlock; }
            set
            {
                _foundTextBlock = value;
                ViewListPosters = new ObservableCollection<Post>((from item in _listPosters where Regex.IsMatch(item.Name.ToLower(), $"{_foundTextBlock.ToLower()}") select item).ToList());
                Notify();
            }
        }

        public ObservableCollection<Post> ViewListPosters
        {
            get { return _viewListPosters; }
            set
            {
                _viewListPosters = value;
                Notify();
            }
        }

        public ObservableCollection<PopupGenre> ViewListGenres
        {
            get { return _viewListGenres; }
            set
            {
                _viewListGenres = value;
                Notify();
            }
        }

        public Post SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                //_selectedPost = value;
                Notify();
                _navigationPages.Parameter = value;
                _navigationPages.ChangePage(new SelectedFilmPageVM());
            }
        }

        public bool PopupIsOpen
        {
            get { return _popupIsOpen; }
            set
            {
                _popupIsOpen = value;
                Notify();
            }
        }

        public RelayCommand FilterGenreBtn
        {
            get
            {
                return new RelayCommand((object? parameter) =>
                {
                    PopupIsOpen = !PopupIsOpen;
                });
            }
        }
    }
}
