using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Model.InteractionServer.ConnactToServer
{
    public interface ICheckConnact
    {
        void Connected();
        void Disconnected();
    }
}
