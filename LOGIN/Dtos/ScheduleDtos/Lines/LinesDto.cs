using LOGIN.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LOGIN.Dtos.ScheduleDtos.Lines
{
    public class LinesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid NeighborhoodsColoniesId { get; set; }
    }
}
