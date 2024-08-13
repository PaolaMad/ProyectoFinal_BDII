using System.ComponentModel.DataAnnotations;

namespace LOGIN.Dtos.ReportDto
{
    public class UpdateReportDto
    {
        public Guid Id { get; set; }  // Identificador del reporte a actualizar
        public IFormFile File { get; set; }  // Archivo de imagen para actualizar

        public string Key { get; set; }

        public string Name { get; set; }


        public string DNI { get; set; }

        public string Cellphone { get; set; }


        public DateTime Date { get; set; }


        public string Report { get; set; }


        public string Direction { get; set; }


        public string Observation { get; set; }

        public Guid StateId { get; set; }
    }
}