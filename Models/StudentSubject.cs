namespace StudentManagement.Models
{
    public class StudentSubject
    {
        public int StudentID { get; set; }
        public required Student Student { get; set; }

        public int SubjectID { get; set; }
        public required Subject Subject { get; set; }
    }
}
