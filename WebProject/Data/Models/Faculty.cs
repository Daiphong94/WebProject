namespace Data.Models
{
    public class Faculty
    {
        public int FacultyID { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string FacultyNumber { get; set; }
        public string Department { get; set; }
        public DateTime JoiningDate { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

    }
}
