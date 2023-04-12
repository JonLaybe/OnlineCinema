using ServerCommandModel.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand
{
    public class ModelCommand : ICommand
    {
        private Action<string, object> _action;
        private Func<string, bool> _func;

        public ModelCommand(Action<string, object> action, Func<string, bool> func)
        {
            _action = action;
            _func = func;
        }
        public bool CheckCommand(string message)
        {
            return _func(message);
        }

        public void Handler(string message, object client)
        {
            if (CheckCommand(message))
                _action(message, client);
        }
    }
}
