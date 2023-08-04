using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = default!;
        
        [Required]
        [StringLength(8, MinimumLength =4)]
        public string Password { get; set; } = default!;
    }
}