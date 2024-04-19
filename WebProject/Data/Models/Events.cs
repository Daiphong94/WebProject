namespace Data.Models
{
    public class Events
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ParticipantCount { get; set; }
        public DateTime StartDate { get; set; }
    }
}
