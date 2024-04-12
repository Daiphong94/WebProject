namespace Data.Models
{
    public class Exam
    {
        public int ExamID { get; set; }
        public int CompetitionID { get; set; }
        public int StudentID { get; set; }
        public float Score { get; set; }
        public int Rank { get; set; }

        public virtual Competition Competition { get; set; }
        public virtual Student Student { get; set; }
    }
}
