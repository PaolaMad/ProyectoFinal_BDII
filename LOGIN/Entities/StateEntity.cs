
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOGIN.Entities
{

    [Table("state", Schema = "reports")]
    public class StateEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid(); // Guid.NewGuid() para generar un nuevo Guid por defecto

        [Required]
        [Column("name")]
        public string Name { get; set; }

    }
}
