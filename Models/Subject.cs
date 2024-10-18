namespace StudentManagement.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public required string SubjectName { get; set; }

        // Many-to-Many relationship
        public required ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
