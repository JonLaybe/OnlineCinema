using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.Command
{
    public interface ICreateServerCommand : IServerCommnad
    {
        string GetCommand(object[] parameter);
    }
}
