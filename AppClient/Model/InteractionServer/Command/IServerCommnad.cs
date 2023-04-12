using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command
{
    public interface IServerCommnad // интерфейс команды
    {
        string Name { get; set; } // название команды
        string Command { get; set; } // команда
    }
}
