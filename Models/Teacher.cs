namespace StudentManagement.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public required string Name { get; set; }

        // One-to-Many relationship
        public required ICollection<Student> Students { get; set; }
    }
}
