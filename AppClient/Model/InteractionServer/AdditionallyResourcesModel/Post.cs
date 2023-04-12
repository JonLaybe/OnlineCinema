using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel
{
    public class Post : CoverPost
    {
        public BitmapImage Photo { get; set; } // Фото
        public int Year { get; set; } // год выпуска кинокартины
        public string Timing { get; set; } // длительность
        public string Description { get; set; } // описание
        public int Price { get; set; } // цена
    }
}
