using JwtWithoutOwin.Models;

namespace JwtWithoutOwin.Services
{
    public class UserService
    {
        internal bool ValidateUser(TokenRequest request)
        {
            return true;
        }

        internal User GetUser(string userName, string type)
        {
            User user = new User();
            if(type.ToUpper() == "SUPPLIER")
            {
                user = new User()
                {
                    Email = "jenny.doe@email.com",
                    Id = 1,
                    MobileNumber = "+1(800)1800-300",
                    PhoneNumber = "+1(800)1800-301",
                    Name = "John Doe"
                };
            }
            else
            {
                user = new User()
                {
                    Email = "john.doe@email.com",
                    Id = 1,
                    MobileNumber = "+1(800)1800-300",
                    PhoneNumber = "+1(800)1800-301",
                    Name = "John Doe"
                };
            }

            return user;
        }
    }
}