using System.ComponentModel.DataAnnotations;

namespace API.DTOS
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength( 20, MinimumLength = 4 )]
        public string Password { get; set; }
    }
}