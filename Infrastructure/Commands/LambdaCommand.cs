using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Infrastructure.Commands
{
    public class LambdaCommand : Command
    {
        private Action<object> _execute;

        private Func<object, bool> _canExucute;

        public LambdaCommand(Action<object> toDo, Func<object, bool> toCheck = null)
        {
            this._execute = toDo;
            this._canExucute = toCheck;
        }
        public override bool CanExecute(object? parameter)
        {
            return _canExucute == null || _canExucute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
