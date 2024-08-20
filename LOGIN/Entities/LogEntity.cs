namespace LOGIN.Entities
{
    public class LogEntity
    {

        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public string Action { get; set; }
        
        public string State { get; set; }

    }
}
