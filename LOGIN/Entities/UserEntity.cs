using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LOGIN.Entities
{
    [Table("user")]
    public class UserEntity : IdentityUser
    {
        //hacer una migracion para arreglar el login
        //[Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        //[Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Column("states")]
        [Range(minimum: 0, maximum: 1)]
        public int Status { get; set; }

        [Column("refresh_token")]
        [StringLength(300)]
        public string? RefreshToken { get; set; }

        [Column("refres-token-date", TypeName = "datetime")]
        public DateTime RefreshTokenDate { get; set; }

        [PersonalData]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }

    }
}
