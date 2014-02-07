using LayerArchitecture.Core.Communication.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayerArchitecture.Core.Communication
{
    public interface IMediator
    {
        Response<TResponseData> Request<TResponseData>(IQuery<TResponseData> query);
        Response<TResponseData> Send<TResponseData>(ICommand<TResponseData> command);
    }
}
