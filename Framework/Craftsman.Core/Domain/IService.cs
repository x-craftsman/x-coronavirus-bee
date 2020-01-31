using Craftsman.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain
{
    /// <summary>
    /// Service 实现标记接口
    /// </summary>
    public interface IService: ITransientDependency
    {
    }
}
