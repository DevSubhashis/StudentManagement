
namespace StudentManagement.Models
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public string? Address { get; set; }
        public DateTime DOB { get; set; }

        // One-to-One relationship
        public int StudentID { get; set; }
        public required Student Student { get; set; }
    }
}
