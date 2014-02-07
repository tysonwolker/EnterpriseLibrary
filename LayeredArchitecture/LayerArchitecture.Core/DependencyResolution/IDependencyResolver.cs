using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayerArchitecture.Core.DependencyResolution
{
    public interface IDependencyResolver
    {
        object GetInstance(Type type);
    }
}
