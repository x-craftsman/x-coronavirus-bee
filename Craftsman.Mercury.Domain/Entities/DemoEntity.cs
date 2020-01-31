using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Mercury.Domain.Entities
{
    public class DemoEntity : IEntity<int>
    {
        public int Id { get; set; }

        public bool IsTransient()
        {
            return true;
        }
    }
}
