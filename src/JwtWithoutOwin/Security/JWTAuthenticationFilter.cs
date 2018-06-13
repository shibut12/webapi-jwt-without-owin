using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JwtWithoutOwin.Security
{
    public class JWTAuthenticationFilter: AuthorizationFilterAttribute
    {
        private AuthenticationModule _authModule;

        public JWTAuthenticationFilter()
        {
            _authModule = new AuthenticationModule();
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!_authModule.IsUserAuthorized(actionContext))
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}