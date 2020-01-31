using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.HardCode
{
    public class HardCodeCurrentUser : ICurrentUser
    {
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public HardCodeCurrentUser()
        {
            this.Id = -100; //HardCode User Id
            this.Name = "HardCode-User"; //HardCode User Name
        }
    }
}
