using LOGIN.Dtos.States;
using LOGIN.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOGIN.Dtos.ReportDto
{
    public class ReportDto
    {

        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string DNI { get; set; }

        public string Cellphone { get; set; }

        public DateTime Date { get; set; }

        public string Report { get; set; }

        public string Direction { get; set; }

        public string Observation { get; set; }

        //public Guid State_Id { get; set; }

        public string PublicId { get; set; }

        public string Url { get; set; }

        //se trae del dto de state
        public StateDto State { get; set; }

    }
}