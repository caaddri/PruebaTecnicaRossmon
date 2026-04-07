using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaRossmonApi.DTOs
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
