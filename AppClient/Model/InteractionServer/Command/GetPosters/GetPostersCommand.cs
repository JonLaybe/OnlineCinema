using OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command.GetPosters
{
    public class GetPostersCommand : ICreateServerCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }

        public GetPostersCommand()
        {
            Name = "GetPoster";
            Command = "GetListPosters";
        }
         
        public string GetCommand(object[] parameter)
        {
            int[] param = parameter.Cast<int>().ToArray();

            if (param != null && param.Count() == 2)
                return $"<{Command}> {param[0]} {param[1]}";
            return $"<{Command}> 0 0";
        }
    }
}
