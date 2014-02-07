using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayerArchitecture.Core.Communication
{
    public interface IQueryHandler<in TQuery, out TResponse>
    where TQuery : IQuery<TResponse>
    {
        TResponse Handle(TQuery query);
    }

}
