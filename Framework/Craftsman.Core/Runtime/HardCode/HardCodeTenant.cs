using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.HardCode
{
    public class HardCodeTenant : ITenant
    {
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public HardCodeTenant()
        {
            this.Id = -999; //HardCode Tenant Id
            this.Name = "HardCode-Tenant"; //HardCode Tenant Name
        }

    }
}
