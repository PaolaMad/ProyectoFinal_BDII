using System.ComponentModel.DataAnnotations;

namespace LOGIN.Dtos.UserDTOs
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        //[Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
    }
}
