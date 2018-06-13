using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace JwtWithoutOwin.Security
{
    public class AuthenticationModule
    {
        private byte[] signingKey;
        private SymmetricSecurityKey securityKey;
        private SigningCredentials signingCredentials;

        public AuthenticationModule()
        {
            signingKey = Convert.FromBase64String("NDI0NzQzZGItZDRlNS00YWNhLTgxYTctYTQyYmY5M2RmM2Iw");
            securityKey = new SymmetricSecurityKey(signingKey);
            signingCredentials = new SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
        }

        internal bool IsUserAuthorized(HttpActionContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext);
            if (authHeader != null)
            {
                JwtSecurityToken userPayloadToken = GenerateUserClaimsFromJWT(authHeader);
                if (userPayloadToken != null)
                {
                    string userName = userPayloadToken.Claims.Where(c => c.Type == ClaimTypes.Name)
                        .Select(c => c.Value).First();
                    string userRole = userPayloadToken.Claims.Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value).First();

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), new string[]{userRole} );

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }

        public string GenerateTokenFromClaims(List<Claim> claims)
        {
            var issuer = "coconutindia";
            var audience = "all";
            var key = signingKey;
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(20);

            var token = new JwtSecurityToken(issuer, audience, claims, now, expires, signingCredentials);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public JwtSecurityToken GenerateUserClaimsFromJWT(string authToken)
        {


            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]{"all"},
                ValidIssuers = new string[]{ "coconutindia" },
                IssuerSigningKey = signingCredentials.Key
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;

            try
            {

                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return null;

            }

            return validatedToken as JwtSecurityToken;
        }
    }
}