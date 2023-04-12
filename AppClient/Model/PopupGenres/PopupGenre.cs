using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineCinema.Model.PopupGenres
{
    public class PopupGenre : Genre
    {
        public ICheckListGenres CheckListGenres { get; set; }
        private bool _isChecked { get; set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                CheckListGenres.UpdateListGenres();
            }
        }
    }
}
