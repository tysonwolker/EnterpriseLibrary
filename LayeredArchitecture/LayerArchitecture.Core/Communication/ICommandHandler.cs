using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayerArchitecture.Core.Communication
{
    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}
