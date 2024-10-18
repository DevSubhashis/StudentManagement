namespace StudentManagement.DTOs
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public required string Name { get; set; }
        public required ProfileDTO Profile { get; set; }
        public required ClassroomDTO Classroom { get; set; }
        public required TeacherDTO Teacher { get; set; }
        public required ICollection<SubjectDTO> Subjects { get; set; }
    }
}
