using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Craftsman.Core.Domain;
using Craftsman.LaunchPad.WebApi.ViewModel;
using Craftsman.LaunchPad.Domain.Services;
using Craftsman.LaunchPad.Domain.Entities;
using Craftsman.Core;

namespace Craftsman.LaunchPad.WebApi.Controllers
{
    /*
     * 实现基于 OAuth2 的授权流程（添加自定义的认证流程）
     *  For authorization: $/oauth2/authorize
     *  For token requests: $/oauth2/token
     *  For refresh token: $/oauth2/refresh
     *  For revoking OAuth tokens: $/oauth2/revoke
     */
    /// <summary>
    /// OAuth2认证服务
    /// </summary>
    [Route("api/oauth2")]
    //[Route("api/consumers")]
    //[Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase, IController
    {

        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpGet]
        //[Route("authorize")]
        public IEnumerable<string> Test()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 基于OAuth2的认证APIs 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("authorize")]
        public IEnumerable<string> Authorize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 基于OAuth2的令牌获取 POST: api/oauth2/token
        /// </summary>
        /// <returns>获取到的令牌</returns>
        [HttpPost]
        [Route("token")]
        public object Token([FromBody]AuthVM model)
        {
            //Some model check here.
            //convert to domain model.
            var context = model.MapTo<AuthContext>();
            return _authService.GenerateToken(context);
        }
        /// <summary>
        /// 基于OAuth2的令牌刷新接口
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("refresh")]
        //[Authorize]
        public IActionResult Refresh()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 基于OAuth2的令牌撤回 POST: api/oauth2/revoke
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpDelete]
        [Route("revoke")]
        //[Authorize]
        public IActionResult Revoke()
        {
            throw new NotImplementedException();
        }
    }
}
