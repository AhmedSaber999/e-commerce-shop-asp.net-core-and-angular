using System.ComponentModel.DataAnnotations;

namespace API.DataShape
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Passowrd { get; set; }
 
    }
}