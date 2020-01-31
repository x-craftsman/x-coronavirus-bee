using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    /// <summary>
    /// No Authorization Creator
    /// </summary>
    public class NoAuthorizationCreator : IServiceComponent, IUserAuthorizationCreator
    {
        /// <summary>
        /// get user authorization.
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>user rights</returns>
        public Dictionary<int, string> GetUserAuthorization(int userId)
        {
            return new Dictionary<int, string>();
        }
    }
}
