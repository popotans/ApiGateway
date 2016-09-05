using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Filters
{
    public interface IFilter
    {
        string FilterType { get; }
        int FilterOrder { get; }
        void Execute();
    }
}
