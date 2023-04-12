using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.GetGenres
{
    public class GetGenresCommand : ICreateServerCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }

        public GetGenresCommand()
        {
            Name = "GetGenres";
            Command = "GetListGenres";
        }

        public string GetCommand(object[] parameter)
        {
            return $"<{Command}>";
        }
    }
}
