using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime
{
    public interface ITenant
    {
        int Id { get; }
        string Name { get; }
    }
}
