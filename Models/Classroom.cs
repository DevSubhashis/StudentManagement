
namespace StudentManagement.Models
{
    public class Classroom
    {
        public int ClassroomID { get; set; }
        public required string ClassroomName { get; set; }

        // One-to-Many relationship
        public required ICollection<Student> Students { get; set; }
    }
}
