using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LOGIN.Dtos.ScheduleDtos.Blocks
{
    public class BlockDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
