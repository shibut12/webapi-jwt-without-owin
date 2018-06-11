using JwtWithoutOwin.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace JwtWithoutOwin.Security
{
    public class TokenController:ApiController
    {
        [HttpPost]
        public HttpResponseMessage post()
        {
            LoginResponseDto dto = new LoginResponseDto()
            {
                AccessToken = Guid.NewGuid().ToString(),
                UserProfile = new User()
                {
                    Email = "test@mail.com",
                    Id = 1,
                    MobileNumber = "+1(800)1800-300",
                    PhoneNumber = "+1(800)1800-301",
                    Name = "John Doe"
                }
            };

            var responseObj = JsonConvert.SerializeObject(dto);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(responseObj, Encoding.UTF8, "application/json");
            return response;
        }
    }
}