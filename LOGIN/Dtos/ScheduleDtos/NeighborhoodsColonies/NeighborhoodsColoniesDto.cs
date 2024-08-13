using LOGIN.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LOGIN.Dtos.ScheduleDtos.NeighborhoodsColonies
{
    public class NeighborhoodsColoniesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BlockId { get; set; }
    }
}
