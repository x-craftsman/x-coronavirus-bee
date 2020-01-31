﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.DapperExtensions
{
    /// <summary>
    /// Optional IgnoreInsert attribute.
    /// Custom for Dapper.SimpleCRUD to exclude a property from Insert methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}
