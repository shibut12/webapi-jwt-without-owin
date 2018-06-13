using JwtWithoutOwin.Models;
using JwtWithoutOwin.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace JwtWithoutOwin.Security
{
    public class TokenController:ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] TokenRequest request)
        {
            HttpResponseMessage response;
            if(ModelState.IsValid)
            {
                UserService service = new UserService();

                if (service.ValidateUser(request))
                {
                    User user = service.GetUser(request.UserName, request.Type);

                    var IdentityClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.NameIdentifier, request.UserName),
                        new Claim(ClaimTypes.Role, request.Type),
                        new Claim(ClaimTypes.MobilePhone, user.MobileNumber)
                    };

                    AuthenticationModule authModule = new AuthenticationModule();
                    string authToken = authModule.GenerateTokenFromClaims(IdentityClaims);

                    LoginResponseDto dto = new LoginResponseDto()
                    {
                        AccessToken = authToken,
                        UserProfile = user
                    };

                    var responseObj = JsonConvert.SerializeObject(dto);

                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(responseObj, Encoding.UTF8, "application/json");
                }
                else
                    response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            
            return response;
        }
    }
}