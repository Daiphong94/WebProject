namespace Data.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int CompetitionID { get; set; }
        public string? File { get; set; }
        public string? Writing { get; set; }

        public virtual Competition Competition { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
