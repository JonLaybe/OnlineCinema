using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.GetListPosters
{
    public class Poster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public byte[] Photo { get; set; }
    }
}
