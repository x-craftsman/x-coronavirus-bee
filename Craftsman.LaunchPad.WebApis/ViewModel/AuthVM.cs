using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.LaunchPad.WebApi.ViewModel
{
    public class AuthVM
    {
        /// <summary>
        /// get or set 'consumer key'
        /// </summary>
        [Required(ErrorMessage = "consumer key 不能为空！")]
        public string ConsumerKey { get; set; }
        /// <summary>
        /// get or set 'consumer secret'
        /// </summary>
        [Required(ErrorMessage = "consumer secret 不能为空！")]
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// get or set 'refresh token'
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// get or set 'code'
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// get or set 'redirect uri'
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// get or set 'grant type'
        /// </summary>
        [Required(ErrorMessage = "grant type 不能为空！")]
        public string GrantType { get; set; }
        /// <summary>
        /// get or set 'user name'
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！")]
        public string UserName { get; set; }
        /// <summary>
        /// get or set 'password'
        /// </summary>
        [Required(ErrorMessage = "用户密码不能为空！")]
        public string Password { get; set; }     
    }

    public class GrantType
    {
        public const string RESOURCE_OWNER_PASSWORD_CREDENTIALS = "password";
        public const string CLIENT_CREDENTIALS = "client_credentials";
        public const string AUTHORIZATION_CODE = "authorization_code";
        public const string IMPLICIT = "token";
        public const string REFRESH_TOKEN = "refresh_token";
    }
}
