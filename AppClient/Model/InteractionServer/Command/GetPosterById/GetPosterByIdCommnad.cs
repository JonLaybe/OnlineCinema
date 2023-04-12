using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.GetPosterById
{
    public class GetPosterByIdCommnad : ICreateServerCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }

        public GetPosterByIdCommnad()
        {
            Name = "GetPosterById";
            Command = "GetPosterById";
        }

        public string GetCommand(object[] parameter)
        {
            int index = Convert.ToInt32(parameter[0]);

            return $"<{Command}> {index}";
        }
    }
}
