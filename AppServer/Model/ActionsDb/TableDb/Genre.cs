using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ActionsDb.TableDb
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } // название жанра
        public virtual List<Film> Films { get; set; } = new(); // фильм
    }
}
