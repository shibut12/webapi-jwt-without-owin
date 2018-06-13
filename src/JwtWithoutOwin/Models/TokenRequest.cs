using System.ComponentModel.DataAnnotations;

namespace JwtWithoutOwin.Models
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Type { get; set; }
    }
}