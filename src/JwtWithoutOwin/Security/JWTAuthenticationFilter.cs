using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JwtWithoutOwin.Security
{
    public class JWTAuthenticationFilter: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
    }
}