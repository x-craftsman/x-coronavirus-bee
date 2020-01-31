using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Runtime;
using Craftsman.LaunchPad.Domain.AuthCenter.Factories;
using Craftsman.LaunchPad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    public abstract class AuthFactory //《聚合根》 - 
    {
        public AuthFactory(ConsumerApp consumer)
        {
            this._consumer = consumer;

            this._userInfoCreator = BuildUserInfomationCreator();
            this._authorizationCreator = BuildUserAuthorizationCreator();
            this._authenticationCreator = BuildUserAuthenticationCreator();
        }
        #region ConsumerApp & Creator Object
        /// <summary>
        /// consumer information
        /// </summary>
        protected ConsumerApp _consumer { get; set; }
        /// <summary>
        /// user information creator.
        /// </summary>
        protected IUserInfomationCreator _userInfoCreator { get; set; }
        /// <summary>
        /// user authorization creator.
        /// </summary>
        protected IUserAuthorizationCreator _authorizationCreator { get; set; }
        /// <summary>
        /// user authentication creator.
        /// </summary>
        protected IUserAuthenticationCreator _authenticationCreator { get; set; }
        #endregion

        #region Abstract method for different application.
        /// <summary>
        /// create user infomation creator.
        /// </summary>
        /// <returns>creator</returns>
        protected abstract IUserInfomationCreator BuildUserInfomationCreator();
        /// <summary>
        /// create user authorization creator.
        /// </summary>
        /// <returns>creator</returns>
        protected abstract IUserAuthorizationCreator BuildUserAuthorizationCreator();
        /// <summary>
        /// create user authentication creator.
        /// </summary>
        /// <returns>creator</returns>
        protected abstract IUserAuthenticationCreator BuildUserAuthenticationCreator();
        #endregion

        #region static create factory.
        /// <summary>
        /// Create AuthUserFactory by ConsumerApp.
        /// </summary>
        /// <param name="consumerApp">ConsumerApp</param>
        /// <returns>AuthUserFactory</returns>
        public static AuthFactory CreateAuthFactory(ConsumerApp consumerApp)
        {
            switch (consumerApp)
            {
                case ConsumerApp.LaunchPad:
                    //TODO: Use Ioc container
                    return new LaunchPadAuthFactory(consumerApp);
                default:
                    return null;
            }
        }
        #endregion static create factory.

        #region Action
        public ICurrentUser AuthenticationUser(string userName, string password)
        {
            var user = this._authenticationCreator.AuthenticationUser(userName, password);
            return user;            
        }
        #endregion
    }
}
