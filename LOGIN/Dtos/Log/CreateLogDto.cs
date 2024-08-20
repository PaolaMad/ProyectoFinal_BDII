namespace LOGIN.Dtos.Log
{
    public class CreateLogDto
    {

        public Guid Id { get; set; }

        public string User_Id { get; set; }

        public DateTime Time { get; set; }

        public string[] Action { get; set; }

        public string State { get; set; }

    }
}
