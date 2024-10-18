
using StudentManagement.Models;

namespace StudentManagement.Repository
{
    public interface IRepository<T> where T : class
    {
        // Basic CRUD operations
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // Custom queries specific to the Student Management System

        // a) Get all students with profile
        Task<IEnumerable<Student>> GetAllStudentsWithProfileAsync();

        // b) Get student with profile by ID
        Task<Student> GetStudentWithProfileByIdAsync(int id);

        // c) Get all students with profile by Classroom ID
        Task<IEnumerable<Student>> GetStudentsByClassroomIdAsync(int classroomId);

        // d) Get all classrooms
        Task<IEnumerable<Classroom>> GetAllClassroomsAsync();

        // e) Get classroom by ID
        Task<Classroom> GetClassroomByIdAsync(int classroomId);

        // f) Get profile by Student ID
        Task<Profile> GetProfileByStudentIdAsync(int studentId);

        // g) Get profile by Profile ID
        Task<Profile> GetProfileByProfileIdAsync(int profileId);

        // h) Get students by Teacher ID
        Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int teacherId);

        // i) Get teacher info by Student ID
        Task<Teacher> GetTeacherByStudentIdAsync(int studentId);

        // j) Get subjects by Student ID
        Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId);

        // k) Get student with profile data by Subject ID
        Task<IEnumerable<Student>> GetStudentsWithProfileBySubjectIdAsync(int subjectId);
    }

}
