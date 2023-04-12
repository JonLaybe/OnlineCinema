using MainServer.Model.ActionsDb.TableDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.GetPosterById
{
    public class SuplementPoster
    {
        public int Id { get; set; }
        public int Price { get; set; } // цена
        public int Year { get; set; } // год выпуска кинокартины
        public string Timing { get; set; } // длительность
        public string Description { get; set; } // описание
    }
}
