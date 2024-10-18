namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public required string Name { get; set; }

        // Relationships
        public int ClassroomID { get; set; }
        public required Classroom Classroom { get; set; }

        public int TeacherID { get; set; }
        public required Teacher Teacher { get; set; }

        public required Profile Profile { get; set; }

        public required ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
