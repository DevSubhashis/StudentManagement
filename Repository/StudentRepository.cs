

// Repository Layer: Handles all the database interactions, 
// including complex relationships between entities.

using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        // CRUD Operations

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id) ?? throw new KeyNotFoundException($"Student with ID {id} was not found.");
        }

        public async Task AddAsync(Student entity)
        {
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Custom Queries

        public async Task<IEnumerable<Student>> GetAllStudentsWithProfileAsync()
        {
            return await _context.Students
                                 .Include(s => s.Profile)
                                 .Include(s => s.Classroom)
                                 .Include(s => s.Teacher)
                                 .Include(s => s.StudentSubjects)
                                 .ThenInclude(ss => ss.Subject)
                                 .ToListAsync();
        }

        public async Task<Student> GetStudentWithProfileByIdAsync(int id)
        {
            var student = await _context.Students
                                 .Include(s => s.Profile)
                                 .Include(s => s.Classroom)
                                 .Include(s => s.Teacher)
                                 .Include(s => s.StudentSubjects)
                                 .ThenInclude(ss => ss.Subject)
                                 .FirstOrDefaultAsync(s => s.StudentID == id);

            if (student == null)
            {
                throw new KeyNotFoundException($"No Student found for Student with ID {id}");
            }

            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassroomIdAsync(int classroomId)
        {
            return await _context.Students
                                 .Include(s => s.Profile)
                                 .Where(s => s.ClassroomID == classroomId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<Classroom> GetClassroomByIdAsync(int classroomId)
        {
            //return await _context.Classrooms.FindAsync(classroomId);

            var classroom = await _context.Classrooms.FindAsync(classroomId);
            if (classroom == null)
            {
                // Log this if necessary
                Console.WriteLine($"Classrooms with ID {classroomId} was not found.");
                throw new KeyNotFoundException($"Classrooms with ID {classroomId} was not found.");
            }
            return classroom;

        }

        public async Task<Profile> GetProfileByStudentIdAsync(int studentId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.StudentID == studentId) ?? throw new KeyNotFoundException($"Student with ID {studentId} was not found.");
        }

        public async Task<Profile> GetProfileByProfileIdAsync(int profileId)
        {
            return await _context.Profiles.FindAsync(profileId) ?? throw new KeyNotFoundException($"Profile with ID {profileId} was not found.");
        }

        public async Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int teacherId)
        {
            return await _context.Students
                                 .Include(s => s.Profile)
                                 .Where(s => s.TeacherID == teacherId)
                                 .ToListAsync();
        }

        public async Task<Teacher> GetTeacherByStudentIdAsync(int studentId)
        {
            var student = await _context.Students
                                        .Include(s => s.Teacher)
                                        .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null || student.Teacher == null)
            {
                throw new KeyNotFoundException($"No Teacher found for Student with ID {studentId}");
            }

            return student.Teacher;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId)
        {
            var studentSubjects = await _context.StudentSubjects
                                                .Where(ss => ss.StudentID == studentId)
                                                .Include(ss => ss.Subject)
                                                .ToListAsync();
            return studentSubjects.Select(ss => ss.Subject);
        }

        public async Task<IEnumerable<Student>> GetStudentsWithProfileBySubjectIdAsync(int subjectId)
        {
            var students = await _context.StudentSubjects
                                         .Where(ss => ss.SubjectID == subjectId)
                                         .Include(ss => ss.Student).ThenInclude(s => s.Profile)
                                         .ToListAsync();
            return students.Select(ss => ss.Student);
        }
    }


}
