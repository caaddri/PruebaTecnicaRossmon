using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaRossmonApi.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }
}
