namespace Data.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

        public ICollection<Admin> Admins { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
