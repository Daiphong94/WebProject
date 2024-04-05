namespace Data.Models
{
    public class Admin
    {
        public int AdminID { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string AdminNumber { get; set; }

        public DateTime JoiningDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
