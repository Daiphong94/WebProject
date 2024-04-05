namespace Data.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        public string StudentNumber { get; set; }
        public string Class { get; set; }
        public string Department { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }


        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<StudentCompetition> StudentCompetitions { get; set; }

        public Student()
        {
            StudentCompetitions = new HashSet<StudentCompetition>();
        }
    }
}
