using System;
using System.Collections.Generic;
using System.Text;
using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Runtime;
using Craftsman.LaunchPad.Domain.Entities;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    public class LaunchPadAuthenticationCreator : IServiceComponent, IUserAuthenticationCreator
    {
        private IRepository<User> _userRepo;

        public LaunchPadAuthenticationCreator(IRepository<User> userRepo)
        {
            this._userRepo = userRepo;
        }
        //public IRepository<User> UserRepo { get; set; }
        public ICurrentUser AuthenticationUser(string loginName, string password)
        {
            var user = _userRepo.FirstOrDefault(x => x.Name == loginName && x.Password == password);
            if (user == null)
            {
                throw new BusinessException("用户名或密码不存在！");
            }

            return user;
        }
    }
}
