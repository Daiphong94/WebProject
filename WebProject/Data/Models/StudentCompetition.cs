namespace Data.Models
{
    public class StudentCompetition
    {
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int CompetitionID { get; set; }
        public Competition Competition { get; set; }

        public DateTime JoinedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
