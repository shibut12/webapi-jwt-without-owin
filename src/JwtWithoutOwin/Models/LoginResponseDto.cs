namespace JwtWithoutOwin.Models
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public User  UserProfile { get; set; }
    }
}