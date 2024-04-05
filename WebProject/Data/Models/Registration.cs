namespace Data.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }
        public int UserID { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
    }
}
