namespace Data.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public int StudentID { get; set; }
        public string? File { get; set; }
        public string? Writing { get; set; }

        public virtual Question Question { get; set; }
        public virtual Student Student { get; set; }
    }
}
