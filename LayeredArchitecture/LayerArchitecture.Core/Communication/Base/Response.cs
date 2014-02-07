using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayerArchitecture.Core.Communication.Base
{
    public class Response<TResponseData>
    {
        public virtual TResponseData Data { get; set; }

        public virtual Exception Exception { get; set; }

        public virtual bool HasException()
        {
            return Exception != null;
        }
    }
}
