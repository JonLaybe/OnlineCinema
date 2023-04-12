using OnlineCinema.Model;
using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using OnlineCinema.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineCinema.ViewModel
{
    public class BuyMoviePageVM : ViewModelBase
    {
        private ActionsNavigationPage _navigationPage;
        private string _numberCard; // номер карты
        private string _cardDate; // день и месяц карты
        private string _cvv; //CVV/CVC
        private string _textPayBtn;

        public BuyMoviePageVM()
        {
            _navigationPage = ActionsNavigationPage.GetActionsNavigationPage();
            Post post = (Post)_navigationPage.Parameter;
            TextPayBtn = $"Оплатить {post.Price} ₽";
        }

        private string RefrashText(string text, char delimiter)
        {
            char temp = text[text.Length - 1];
            StringBuilder sb = new StringBuilder(text);
            sb[text.Length - 1] = delimiter;
            sb.Append(temp);

            return sb.ToString();
        }

        public string TextBlockNumberCard // номер карты
        {
            get { return _numberCard; }
            set
            {
                _numberCard = value;
                if (_numberCard.Length > 0)
                {
                    if (char.IsDigit(_numberCard[_numberCard.Length - 1]))
                    {
                        if (_numberCard.Length % 5 == 0)
                        {
                            _numberCard = RefrashText(_numberCard, ' ');
                        }
                    }
                    else
                    {
                        _numberCard = _numberCard.Remove(_numberCard.Length - 1);
                    }
                    Notify();
                }
            }
        }
        public string CardDate // номер карты
        {
            get { return _cardDate; }
            set
            {
                _cardDate = value;
                if (_cardDate.Length > 0)
                {
                    if (char.IsDigit(_cardDate[_cardDate.Length - 1]))
                    {
                        if (_cardDate.Length == 3)
                        {
                            _cardDate = RefrashText(_cardDate, '/');
                        }
                    }
                    else
                    {
                        _cardDate = _cardDate.Remove(_cardDate.Length - 1);
                    }
                    Notify();
                }
            }
        }
        public string CVV
        {
            get { return _cvv; }
            set
            {
                _cvv = value;
                if (_cvv.Length > 0 && !char.IsDigit(_cvv[_cvv.Length - 1]))
                {
                    _cvv.Remove(_cvv.Length - 1, 1);
                }
                Notify();
            }
        }
        public string TextPayBtn
        {
            get { return _textPayBtn; }
            set
            {
                _textPayBtn = value;
                Notify();
            }
        }

        public RelayCommand PayBtn // кнопка оплаты
        {
            get
            {
                return new RelayCommand((object parameter) =>
                {
                    _navigationPage.ChangePage(new MainPageVM());
                });
            }
        }
        public RelayCommand BackBtn
        {
            get
            {
                return new RelayCommand((object? parameter) =>
                {
                    _navigationPage.ChangePage(new SelectedFilmPageVM());
                });
            }
        }
    }
}
