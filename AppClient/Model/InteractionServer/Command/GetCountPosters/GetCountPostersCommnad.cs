using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.GetCountPosters
{
    public class GetCountPostersCommnad : ICreateServerCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }

        public GetCountPostersCommnad()
        {
            Name = "GetCountPosters";
            Command = "GetCountPosters";
        }

        public string GetCommand(object[] parameter)
        {
            return $"<{Command}>";
        }
    }
}
