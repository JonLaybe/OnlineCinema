using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ActionsDb.TableDb
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; } // имя кинокартины
        public string Photo { get; set; } // постер
        public int Price { get; set; } // цена
        public int Year { get; set; } // год выпуска кинокартины
        public string Timing { get; set; } // длительность
        public string Description { get; set; } // описание
        public virtual List<Genre> Genres { get; set; } = new(); //жанры
    }
}
