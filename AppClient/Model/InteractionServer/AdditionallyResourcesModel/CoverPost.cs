using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel
{
    public class CoverPost
    {
        public int Id { get; set; } // id
        public string Name { get; set; } // название фильма
        public List<string> Genres { get; set; } // жанры
        public byte[] Photo { get; set; } // фото
    }
}
