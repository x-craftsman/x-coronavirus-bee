using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Dependency
{
    /// <summary>
    /// 实现此接口的所有类都自动注册到依赖项注入作为 Self 类型。（此接口会有多个实现-多态实现）
    /// </summary>
    public interface IPolymorphismDependency
    {
    }
}
