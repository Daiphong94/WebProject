namespace Data.Models
{
    public class Response
    {
        public int ResponseID { get; set; }
        public int SurveyID { get; set; }
        public string Answers { get; set; }
        public DateTime SubmissionDate { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
