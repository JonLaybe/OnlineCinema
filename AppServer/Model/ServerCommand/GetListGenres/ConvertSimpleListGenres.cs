using MainServer.Model.ActionsDb.TableDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.GetListGenres
{
    public class ConvertSimpleListGenres
    {
        public int Id { get; set; }
        public string Name { get; set; } // название жанра
    }
}
